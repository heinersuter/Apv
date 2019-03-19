using System;
using System.Collections.Generic;
using System.Linq;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Email;

namespace Apv.MemberExcel.Views
{
    public class EmailPreviewViewModel : ViewModel
    {
        private EmailDto[] _emailDtos;

        public int Index
        {
            get { return BackingFields.GetValue<int>(); }
            set { BackingFields.SetValue(value); }
        }

        public int Count
        {
            get { return BackingFields.GetValue<int>(); }
            set { BackingFields.SetValue(value); }
        }

        public string To
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public string Subject
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public string Body
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public string Attachements
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public DelegateCommand ShowPreviousCommand => BackingFields.GetCommand(ShowPrevious, CanShowPrevious);

        public DelegateCommand ShowNextCommand => BackingFields.GetCommand(ShowNext, CanShowNext);

        public void SetEmails(IEnumerable<EmailDto> emailDtos)
        {
            _emailDtos = emailDtos.ToArray();
            Count = _emailDtos.Length;
            UpdateView(1);
        }

        public void UpdateView(int newIndex)
        {
            if (_emailDtos != null && _emailDtos.Any())
            {
                if (newIndex < 1 || newIndex > _emailDtos.Length)
                {
                    UpdateView(1);
                }
                else
                {
                    To = _emailDtos[newIndex - 1].To;
                    Subject = _emailDtos[newIndex - 1].Subject;
                    Body = _emailDtos[newIndex - 1].Body;
                    Attachements = string.Join(Environment.NewLine, _emailDtos[newIndex - 1].Attachements);
                    Index = newIndex;
                }
            }
            else
            {
                To = null;
                Subject = null;
                Body = null;
                Attachements = null;
                Index = 0;
            }
        }

        private bool CanShowPrevious()
        {
            return _emailDtos != null && Index > 1;
        }

        private void ShowPrevious()
        {
            UpdateView(Index - 1);
        }

        private bool CanShowNext()
        {
            return _emailDtos != null && Index < _emailDtos.Length;
        }

        private void ShowNext()
        {
            UpdateView(Index + 1);
        }
    }
}
