using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extractor
{
    class Program
    {
        static string OrNull(int? value) => value.HasValue ? value.ToString() : "NULL";
        static string Escape(string value) => value == null ? "NULL" : "'" + value.Replace("'", "''") + "'";

        static void Main(string[] args)
        {


            var sections = ProcessMeyer();
            var fileName1 = "Meyer1.sql";
            var fileName2 = "Meyer2.sql";


            ProcessFiles(sections);

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
                        sqlB.AppendLine($"(),");
                }

                Console.WriteLine(sqlB.ToString());
                File.AppendAllText(fileName2, sqlB.ToString());
            }

        }
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




        static List<Section> ProcessMeyer()
        {
            var sections = new List<Section>();
            var bookKey = 1;

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

            return sections;
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            ////Dump Results
            //foreach (var section in sections)
            //{
            //    Console.WriteLine($"Parent {section.ParentSectionKey} Section {section.SectionKey}: {section.SectionName}");
            //    foreach (var play in section.Plays)
            //    {
            //        Console.WriteLine($"\t Play: {play.PlayName} Page: {play.PageReference}");
            //    }
            //}


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
        static void ProcessSection(string[] lines, ref int currentLineIndex, int bookKey, Section currentSection, int depth, List<Section> sections)
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
                        ParentSectionKey = currentSection.SectionKey,
                        SectionKey = sections.Max(x => x.SectionKey) + 1,
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
                else if (currentLine.StartsWith(playHeader))
                {
                    currentLine = currentLine.Replace(playHeader, "").Replace(playFooter, "");//strip the headers
                    var split = currentLine.Split('|');
                    var play = new Section()
                    {
                        FileName = split[0],
                        SectionName = split[1],
                        SectionKey = sections.Max(x => x.SectionKey) + 1,
                        ParentSectionKey = currentSection.SectionKey,
                        DisplayOrder = displayOrder,
                        PageReference = "XXX",
                        BookKey=bookKey
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
        static void ProcessFiles(List<Section> sections)
        {
            //TODO
        }
    }

    class Section
    {
        public int BookKey { get; set; }
        public int SectionKey { get; set; }
        public int? ParentSectionKey { get; set; }
        public string SectionName { get; set; }

        public string PageReference { get; set; }
        public string FileName { get; set; }
        public int DisplayOrder { get; set; }
        public List<Video> Videos { get; } = new List<Video>();
    }

    class Video
    {
        public int VideoKey { get; set; }
        public int SectionKey { get; set; }
        public string YouTubeId { get; set; }
        public string Url { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int CreatedByUserKey { get; set; } = 1;



    }

}
