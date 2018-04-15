using HemaVideoTools.Services;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tortuga.Anchor.Collections;

namespace HemaVideoTools
{
	public class MainViewModel : Tortuga.Sails.ViewModelBase
	{
		private readonly Client m_ApiClient;

		public MainViewModel(Client apiClient)
		{
			m_ApiClient = apiClient;
			PropertyChanged += MainViewModel_PropertyChanged;
		}

		public ICommand AddPlayCommand => GetCommand(AddPlay);
		public BookSummary Book { get => Get<BookSummary>(); set => Set(value); }
		public WindowState WindowState { get => Get<WindowState>(); set => Set(value); }
		public BookDetail BookDetail { get => Get<BookDetail>(); set => Set(value); }
		public ObservableCollectionExtended<BookSummary> BookList => GetNew<ObservableCollectionExtended<BookSummary>>();
		public ICommand CopyMarkedPlayCommand => GetCommand(CopyMarkedPlay);
		public ICommand CopyPlayCommand => GetCommand<PlayDetail>(CopyPlay);
		public ICommand EditPlayCommand => GetCommand<PlayDetail>(EditPlay);
		public ICommand LoadTagsCommand => GetCommand(async () => await LoadTagsAsync());
		public PlayDetail MarkedPlay { get => Get<PlayDetail>(); set => Set(value); }
		public bool TagsLoading { get => Get<bool>(); set => Set(value); }
		public ICommand MarkPlayCommand => GetCommand<PlayDetail>(MarkPlay);
		public SectionSummary Section { get => Get<SectionSummary>(); set => Set(value); }
		public SectionDetail SectionDetail { get => Get<SectionDetail>(); set => Set(value); }
		public Tags Tags { get => Get<Tags>(); set => Set(value); }

		public async Task LoadBooksAsync()
		{
			BookList.Clear();
			BookList.AddRange(await m_ApiClient.ApiBookGetAsync());
		}

		public async Task LoadTagsAsync()
		{
			TagsLoading = true;
			var techniques = (await m_ApiClient.ApiTagTechniqueGetAsync()).OrderBy(x => x.TechniqueName);
			var guards = (await m_ApiClient.ApiTagGuardGetAsync()).OrderBy(x => x.GuardName);
			var footwork = (await m_ApiClient.ApiTagFootworkGetAsync()).OrderBy(x => x.FootworkName);
			var targets = (await m_ApiClient.ApiTagTargetGetAsync()).OrderBy(x => x.TargetName);
			var guardModifiers = (await m_ApiClient.ApiTagGuardModifierGetAsync()).OrderBy(x => x.GuardModifierName);
			var measures = (await m_ApiClient.ApiTagMeasureGetAsync()).OrderBy(x => x.MeasureName);

			Tags = new Tags(footwork, techniques, targets, guards, guardModifiers, measures);
			TagsLoading = false;
		}

		public async Task RefreshSectionDetailAsync()
		{
			if (Section?.SectionKey != null)
				SectionDetail = await m_ApiClient.ApiBookByBookKeyBySectionKeyGetAsync(Section.SectionKey.Value, Section.BookKey.ToString());
		}

		void AddPlay()
		{
			var vm = new PlayEditorViewModel(m_ApiClient, Tags, SectionDetail, RefreshSectionDetailAsync);
			ShowPlayEditor(vm);
		}

		void CopyMarkedPlay()
		{
			var vm = new PlayEditorViewModel(m_ApiClient, Tags, SectionDetail, RefreshSectionDetailAsync, MarkedPlay, true);
			ShowPlayEditor(vm);
		}

		void CopyPlay(PlayDetail play)
		{
			var vm = new PlayEditorViewModel(m_ApiClient, Tags, SectionDetail, RefreshSectionDetailAsync, play, true);
			ShowPlayEditor(vm);
		}

		void EditPlay(PlayDetail play)
		{
			var vm = new PlayEditorViewModel(m_ApiClient, Tags, SectionDetail, RefreshSectionDetailAsync, play, false);
			ShowPlayEditor(vm);
		}

		private void ShowPlayEditor(PlayEditorViewModel vm)
		{
			var window = new PlayEditor() { DataContext = vm };
			if (WindowState == WindowState.Maximized)
				window.WindowState = WindowState.Maximized;
			window.Show();
		}

		private async void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == null || e.PropertyName == nameof(Book))
			{
				if (Book?.BookKey != null)
				{
					BookDetail = await m_ApiClient.ApiBookByBookKeyDetailGetAsync(Book.BookKey.Value);
				}
				else
					BookDetail = null;
			}
			if (e.PropertyName == null | e.PropertyName == nameof(Section))
			{
				if (Section?.SectionKey != null)
					SectionDetail = await m_ApiClient.ApiBookByBookKeyBySectionKeyGetAsync(Section.SectionKey.Value, Section.BookKey.ToString());
				else
					SectionDetail = null;
			}
		}

		void MarkPlay(PlayDetail play)
		{
			MarkedPlay = play;
		}
	}
}