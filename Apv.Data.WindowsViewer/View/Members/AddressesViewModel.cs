using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class AddressesViewModel : ViewModel
    {
        public MemberDetailsDto Member
        {
            get { return BackingFields.GetValue<MemberDetailsDto>(); }
            set { BackingFields.SetValue(value, member => { AddCommand.RaiseCanExecuteChanged(); DeleteCommand.RaiseCanExecuteChanged(); }); }
        }

        public DelegateCommand AddCommand => BackingFields.GetCommand(Add, CanAdd);

        private bool CanAdd()
        {
            return Member?.Addresses?.Count <= 0;
        }

        private void Add()
        {
            Member.Addresses.Add(new AddressDto());
            RaisePropertyChanged(() => Member);
        }

        public DelegateCommand<AddressDto> DeleteCommand => BackingFields.GetCommand<AddressDto>(Delete, CanDelete);

        private bool CanDelete(AddressDto address)
        {
            return Member?.Addresses?.Count > 0;
        }

        private void Delete(AddressDto address)
        {
            Member.Addresses.Remove(address);
            RaisePropertyChanged(() => Member);
        }
    }
}
