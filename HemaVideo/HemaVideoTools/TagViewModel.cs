using HemaVideoTools.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tortuga.Sails;

namespace HemaVideoTools
{
	public class TagViewModel<T> : ViewModelBase
		where T : ITag, new()
	{
		readonly Client m_ApiClient;

		public TagViewModel(Client apiClient)
		{
			m_ApiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
			Tag = new T();
		}

		public TagViewModel(Client apiClient, T tag)
		{
			m_ApiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
			Tag = tag;
		}

		public ICommand CancelCommand => GetCommand<Window>(dialog => Cancel(dialog));
		public ICommand SaveCommand => GetCommand<TagEditorDialog>(async (dialog) => await SaveAsync(dialog));
		public T Tag { get; }

		void Cancel(Window dialog)
		{
			dialog.DialogResult = false;
			dialog.Close();
		}

		async Task SaveAsync(Window dialog)
		{
			try
			{
				switch (Tag)
				{
					case Measure m:
						Tag.Key = await m_ApiClient.ApiTagMeasureAddPostAsync(m);
						break;

					case Guard g:
						Tag.Key = await m_ApiClient.ApiTagGuardAddPostAsync(g);
						break;

					case GuardModifier gm:
						Tag.Key = await m_ApiClient.ApiTagGuardModifierAddPostAsync(gm);
						break;

					case Footwork f:
						Tag.Key = await m_ApiClient.ApiTagFootworkAddPostAsync(f);
						break;

					case Technique t:
						Tag.Key = await m_ApiClient.ApiTagTechniqueAddPostAsync(t);
						break;

					case Target t:
						Tag.Key = await m_ApiClient.ApiTagTargetAddPostAsync(t);
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failed to save record");
			}
			dialog.DialogResult = true;
			dialog.Close();
		}
	}
}