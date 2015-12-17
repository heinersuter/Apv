using System.Collections.Generic;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.View.Members
{
    public abstract class MemberItemsViewModel<T> : ViewModel
    {
        public List<T> Items
        {
            get { return BackingFields.GetValue<List<T>>(); }
            protected set { BackingFields.SetValue(value); }
        }

        public MemberDetailsDto Member
        {
            get { return BackingFields.GetValue<MemberDetailsDto>(); }
            set { BackingFields.SetValue(value, OnMemberChanged); }
        }

        protected virtual void OnMemberChanged(MemberDetailsDto member)
        {
            Items = new List<T>(GetItems());
            AddCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand AddCommand => BackingFields.GetCommand(Add, CanAdd);

        protected virtual bool CanAdd()
        {
            return true;
        }

        protected virtual void Add()
        {
            Items = new List<T>(GetItems());
        }

        public DelegateCommand<AddressDto> DeleteCommand => BackingFields.GetCommand<AddressDto>(Delete, CanDelete);

        protected virtual bool CanDelete(AddressDto address)
        {
            return true;
        }

        protected virtual void Delete(AddressDto address)
        {
            Items = new List<T>(GetItems());
        }

        protected abstract ICollection<T> GetItems();
    }
}
