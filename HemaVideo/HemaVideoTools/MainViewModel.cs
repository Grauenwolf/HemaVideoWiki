using HemaVideoTools.Services;
using System.ComponentModel;
using System.Threading.Tasks;
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

		public BookSummary Book { get => Get<BookSummary>(); set => Set(value); }
		public BookDetail BookDetail { get => Get<BookDetail>(); set => Set(value); }
		public ObservableCollectionExtended<BookSummary> BookList => GetNew<ObservableCollectionExtended<BookSummary>>();
		public SectionSummary Section { get => Get<SectionSummary>(); set => Set(value); }
		public SectionDetail SectionDetail { get => Get<SectionDetail>(); set => Set(value); }

		public async Task LoadBooksAsync()
		{
			BookList.Clear();
			BookList.AddRange(await m_ApiClient.ApiBookGetAsync());
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
	}
}