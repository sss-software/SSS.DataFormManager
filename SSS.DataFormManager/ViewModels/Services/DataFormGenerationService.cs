using SSS.DataFormManager.Models;
using System;
using System.Collections.ObjectModel;

namespace SSS.DataFormManager.ViewModels.Services
{
    public class DataFormGenerationService
    {
        public DataFormDTO CreateDataFormDTO(ObservableCollection<DataFormEntry> DataFormEntries)
        {
            DataFormDTO dto = new DataFormDTO();
            var header = new DataFormHeader()
            {
                LocalId = -1,
                CloudId = -1,
                UniqueId = Guid.NewGuid(),
                Title = "Form Title",
                Description = "Form Description",
                IsEncrypted = true
            };
            var footer = new DataFormFooter()
            {
                CapturedBy = "Neil Slambert",
                CapturedOn = DateTime.Now,
                Note = "None"
            };
            var entries = new ObservableCollection<DataEntry>();
            foreach (DataFormEntry entry in DataFormEntries)
            {
                DataEntry data = new DataEntry()
                {
                    LabelControl = entry.LabelControl,
                    LabelValue = entry.LabelValue,
                    DataControl = entry.DataControl,
                    TextBoxControlValue = entry.TextBoxControlValue,
                    ListBoxControlItemsSource = entry.ListBoxItemsSource,
                    ListBoxControlSelectedItem = entry.ListBoxSelectedItem,
                    ListBoxControlSelectedIndex = entry.ListBoxSelectedIndex,
                    ListBoxControlSelectedValue = entry.ListBoxControlValue,
                    ComboBoxControlItemsSource = entry.ComboBoxItemsSource,
                    ComboBoxControlSelectedItem = entry.ComboBoxSelectedItem,
                    ComboBoxControlSelectedIndex = entry.ComboBoxSelectedIndex,
                    ComboBoxControlSelectedValue = entry.ComboBoxControlValue,
                };
                entries.Add(data);
            }

            var body = new DataFormBody()
            {
                DataEntries = entries
            };
            dto.DataFormHeader = header;
            dto.DataFormFooter = footer;
            dto.DataFormBody = body;
            return dto;
        }
    }
}