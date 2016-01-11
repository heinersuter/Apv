using System.Collections.Generic;

using Apv.Data.Dtos;
using Apv.Data.Dtos.Members;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class NotesViewModel : MemberItemsViewModel<NoteDto>
    {
        protected override ICollection<NoteDto> MemberItems => Member.Notes;

        protected override NoteDto CreateItem()
        {
            return new NoteDto();
        }
    }
}
