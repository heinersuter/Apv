using System;
using System.Collections.Generic;
using System.Linq;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;
using Apv.Data.Model;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class BaseMemberDataViewModel : ViewModel
    {
        public static IEnumerable<MemberStatus> Statuses => Enum.GetValues(typeof(MemberStatus)).Cast<MemberStatus>();

        public static IEnumerable<string> Genders => new[] { null, "Male", "Female" };

        public MemberDetailsDto Member
        {
            get { return BackingFields.GetValue<MemberDetailsDto>(); }
            set { BackingFields.SetValue(value); }
        }
    }
}
