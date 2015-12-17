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
            UpdateItems();
        }

        public DelegateCommand AddCommand => BackingFields.GetCommand(Add, CanAdd);

        protected virtual bool CanAdd()
        {
            return true;
        }

        protected virtual void Add()
        {
            MemberItems.Add(CreateItem());
            UpdateItems();
        }

        public DelegateCommand<T> DeleteCommand => BackingFields.GetCommand<T>(Delete, CanDelete);

        protected virtual bool CanDelete(T item)
        {
            return Items.Count > 0;
        }

        protected virtual void Delete(T item)
        {
            MemberItems.Remove(item);
            UpdateItems();
        }

        protected abstract ICollection<T> MemberItems { get; }

        protected abstract T CreateItem();

        private void UpdateItems()
        {
            Items = new List<T>(MemberItems);
            AddCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }
    }
}
