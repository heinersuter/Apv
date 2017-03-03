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
        private int _index;

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
            UpdateView(0);
        }

        public void UpdateView(int index)
        {
            if (_emailDtos != null)
            {
                if (index < 0 || index >= _emailDtos.Length)
                {
                    UpdateView(0);
                }
                else
                {
                    To = _emailDtos[index].To;
                    Subject = _emailDtos[index].Subject;
                    Body = _emailDtos[index].Body;
                    Attachements = string.Join(Environment.NewLine, _emailDtos[index].Attachements);
                    _index = index;
                }
            }
            else
            {
                To = null;
                Subject = null;
                Body = null;
                Attachements = null;
            }
        }

        private bool CanShowPrevious()
        {
            return _emailDtos != null && _index > 0;
        }

        private void ShowPrevious()
        {
            UpdateView(_index - 1);
        }

        private bool CanShowNext()
        {
            return _emailDtos != null && _index < _emailDtos.Length - 1;
        }

        private void ShowNext()
        {
            UpdateView(_index + 1);
        }
    }
}
