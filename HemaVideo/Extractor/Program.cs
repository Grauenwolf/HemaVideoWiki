using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Extractor
{
    class Program
    {
        public static void Main(string[] args)
        {

            ProcessMeyer();
            ProcessI33();

        }

        static string Escape(string value) => value == null ? "NULL" : "'" + value.Replace("'", "''") + "'";

        static void GenerateSql(IList<Section> sections, string fileName1, string fileName2)
        {
            Console.WriteLine();
            Console.WriteLine();

            {
                var sqlA = new StringBuilder();
                foreach (var section in sections)
                {
                    sqlA.AppendLine($"({section.SectionKey},{section.BookKey},{OrNull(section.ParentSectionKey)},{Escape(section.SectionName)},{Escape(section.PageReference)},{section.DisplayOrder}),");
                }

                Console.WriteLine(sqlA.ToString());
                File.WriteAllText(fileName1, sqlA.ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
            {
                var sqlB = new StringBuilder();
                foreach (var section in sections)
                {
                    foreach (var video in section.Videos)
                        sqlB.AppendLine($"({section.SectionKey},{video.VideoServiceKey},{Escape(video.VideoServiceVideoId)}),");
                }

                Console.WriteLine(sqlB.ToString());
                File.AppendAllText(fileName2, sqlB.ToString());
            }
        }
        static string OrNull(int? value) => value.HasValue ? value.ToString() : "NULL";
        /*
* 
* 
* VALUES
(   0,   -- PlayKey - int
0,   -- SectionKey - int
N'', -- PlayName - nvarchar(250)
N'', -- PageReference - nvarchar(50)
0.0  -- DisplayOrder - float
);
* 
* */




        static void ProcessFiles(IList<Section> sections, string folder)
        {
            foreach (var section in sections.Where(x => x.FileName != null))
            {
                var file = new FileInfo(Path.Combine(folder, section.FileName));
                if (file.Exists)
                {
                    foreach (var line in File.ReadAllLines(file.FullName))
                    {
                        var start = line.IndexOf("[[media type");
                        if (start == -1)
                            continue;

                        var end = line.IndexOf("]]");
                        var mediaTag = line.Substring(start + 2 + 6, end - start - 2 - 6);
                        Console.WriteLine(mediaTag);
                        var video = new Video() { SectionKey = section.SectionKey };
                        var mediaParts = mediaTag.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var part in mediaParts)
                        {
                            var binary = part.Split('=');
                            if (binary[0] == "type")
                            {
                                video.VideoServiceName = binary[1].Replace("\"", "");
                            }
                            if (binary[0] == "key")
                            {
                                video.VideoServiceVideoId = binary[1].Replace("\"", "");
                            }
                        }

                        switch (video.VideoServiceName)
                        {
                            case "youtube":
                                video.VideoServiceKey = 1;
                                break;
                            case "vimeo":
                                video.VideoServiceKey = 2;
                                break;
                            case "custom":
                                continue; //can't handle this one
                            default:
                                Console.WriteLine("Unknown video type: " + video.VideoServiceName);
                                Console.ReadLine();
                                break;
                        }

                        section.Videos.Add(video);
                    }
                }
            }


        }

        static void ProcessI33()
        {
            var bookKey = 2;
            var sections = new SectionCollection(bookKey);


            var indexFileName = $@"I.33\MS I.33";
            var lines = File.ReadAllLines(indexFileName);
            var currentLineIndex = 0;

            ProcessSection(lines, ref currentLineIndex, bookKey, null, 1, sections);


            ProcessFiles(sections, "I.33");

            var fileName1 = "I.33-1.sql";
            var fileName2 = "I.33-2.sql";

            GenerateSql(sections, fileName1, fileName2);

        }

        static void ProcessMeyer()
        {
            var bookKey = 1;
            var sections = new SectionCollection(bookKey);

            //Process Index Files
            for (var indexFileIndex = 1; indexFileIndex <= 5; indexFileIndex++)
            {
                var indexFileName = $@"Meyer\Meyer 1570 Book {indexFileIndex}";
                var lines = File.ReadAllLines(indexFileName);
                var currentLineIndex = 0;

                var section = new Section()
                {
                    BookKey = bookKey,
                    ParentSectionKey = null,
                    SectionKey = sections.Any() ? sections.Max(x => x.SectionKey) + 1 : 1,
                };
                sections.Add(section);

                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    if (line.StartsWith("==") && !line.StartsWith("==="))
                    {
                        section.SectionName = line.Replace("==", "");
                        currentLineIndex = i + 1;
                        break;
                    }
                }

                ProcessSection(lines, ref currentLineIndex, bookKey, section, 2, sections);
            }

            ProcessFiles(sections, "Meyer");

            var fileName1 = "Meyer1.sql";
            var fileName2 = "Meyer2.sql";

            GenerateSql(sections, fileName1, fileName2);

        }

        /// <summary>
        /// Processes the child rows in a section.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <param name="currentLineIndex">Index of the current line.</param>
        /// <param name="bookKey">The book key.</param>
        /// <param name="parentSectionKey">The SectionKey for the section currently being processed.</param>
        /// <param name="depth">The depth of the section currently being processed.</param>
        /// <param name="sections">The sections.</param>
        static void ProcessSection(string[] lines, ref int currentLineIndex, int bookKey, Section currentSection, int depth, SectionCollection sections)
        {
            const string playHeader = "[[";
            const string playFooter = "]]";
            string childSectionHeader = new string('=', depth + 1);
            string nextSectionHeader = new string('=', depth);
            int displayOrder = 1;

            while (currentLineIndex < lines.Length)
            {
                var currentLine = lines[currentLineIndex].Trim();
                if (currentLine.StartsWith(childSectionHeader))
                {
                    //create section
                    var childSection = new Section()
                    {
                        BookKey = bookKey,
                        ParentSectionKey = currentSection?.SectionKey,
                        SectionKey = sections.NextSectionKey,
                        SectionName = currentLine.Replace(childSectionHeader, ""),
                        DisplayOrder = displayOrder
                    };
                    sections.Add(childSection);
                    currentLineIndex += 1;
                    displayOrder += 1;

                    //process section
                    ProcessSection(lines, ref currentLineIndex, bookKey, childSection, depth + 1, sections);

                    continue; //restart the loop with the new current line index
                }
                else if (currentLine.StartsWith(nextSectionHeader))
                {
                    //currentLineIndex += 1; don't increment the counter, we'll pick this up in the parent
                    return;
                }
                else if (currentLine.StartsWith("[[toc]]"))
                {
                    //skip
                }
                else if (currentLine.StartsWith(playHeader))
                {
                    currentLine = currentLine.Replace(playHeader, "").Replace(playFooter, "");//strip the headers
                    var split = currentLine.Split('|');
                    var play = new Section()
                    {
                        FileName = split[0],
                        SectionName = split[1],
                        SectionKey = sections.NextSectionKey,
                        ParentSectionKey = currentSection?.SectionKey,
                        DisplayOrder = displayOrder,
                        PageReference = "XXX",
                        BookKey = bookKey
                    };
                    displayOrder += 1;

                    if (play.SectionName.Contains("(") && play.SectionName.Contains(")"))
                    {
                        var startIndex = play.SectionName.IndexOf("(") + 1;
                        var endIndex = play.SectionName.IndexOf(")") - 1;
                        var length = endIndex - startIndex + 1;
                        var pageRef = play.SectionName.Substring(startIndex, length);

                        if (char.IsDigit(pageRef[0]))
                        {
                            play.PageReference = pageRef;
                            play.SectionName = play.SectionName.Substring(0, startIndex - 2).Trim();
                        }
                    }

                    sections.Add(play);
                }

                currentLineIndex += 1;
            }
        }
    }
}
