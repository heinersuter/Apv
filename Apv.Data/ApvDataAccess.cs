﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Apv.Data.Dtos.Members;
using Apv.Data.Model;
using Apv.Data.Model.Members;

namespace Apv.Data
{
    public class ApvDataAccess
    {
        private readonly string _connectionString;

        public ApvDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<MemberDto> GetMembers()
        {
            using (var context = new ApvDbContext(_connectionString))
            {
                var members = context.Members
                    .OrderBy(member => member.Nickname)
                    .ThenBy(member => member.Lastname)
                    .ThenBy(member => member.Firstname)
                    .ToArray();
                return members.Select(DtoHelper.ToDto);
            }
        }

        public MemberDetailsDto GetMemberDetails(long memberId)
        {
            using (var context = new ApvDbContext(_connectionString))
            {
                var member = GetMemberWithDetailsById(memberId, context);
                return DtoHelper.ToDetailsDto(member);
            }
        }

        private Member GetMemberWithDetailsById(long memberId, ApvDbContext context)
        {
            return context.Members.Where(m => m.Id == memberId)
                                  .Include(m => m.Addresses)
                                  .Include(m => m.EmailAddresses)
                                  .Include(m => m.PhoneNumbers)
                                  .Include(m => m.Notes)
                                  .Include(m => m.Functions)
                                  .Include(m => m.Communication)
                                  .SingleOrDefault();
        }

        public void UpdateMember(MemberDetailsDto memberDto)
        {
            var newEntity = DtoHelper.FromDto(memberDto);

            using (var context = new ApvDbContext(_connectionString))
            {
                var exisingEntity = GetMemberWithDetailsById(memberDto.Id, context);
                if (exisingEntity == null)
                {
                    context.Members.Add(newEntity);
                }
                else
                {
                    context.Entry(exisingEntity).CurrentValues.SetValues(newEntity);

                    UpdateChildren(newEntity.Addresses, exisingEntity.Addresses, context.Addresses, context);
                    UpdateChildren(newEntity.EmailAddresses, exisingEntity.EmailAddresses, context.EmailAddresses, context);
                    UpdateChildren(newEntity.PhoneNumbers, exisingEntity.PhoneNumbers, context.PhoneNumbers, context);
                    UpdateChildren(newEntity.Notes, exisingEntity.Notes, context.Notes, context);
                    UpdateChildren(newEntity.Functions, exisingEntity.Functions, context.Functions, context);

                    context.Entry(exisingEntity.Communication).CurrentValues.SetValues(newEntity.Communication);
                }

                context.SaveChanges();
            }
        }

        private static void UpdateChildren<T>(ICollection<T> newEntities, ICollection<T> existingEntities, IDbSet<T> entities, DbContext context) where T : Item
        {
            foreach (var existingEntity in existingEntities.ToArray())
            {
                if (newEntities.All(entity => entity.Id != existingEntity.Id))
                {
                    entities.Remove(existingEntity);
                }
            }

            foreach (var newEntity in newEntities)
            {
                var exisingEntity = existingEntities.SingleOrDefault(entity => entity.Id == newEntity.Id);
                if (newEntity.Id == 0 || exisingEntity == null)
                {
                    existingEntities.Add(newEntity);
                }
                else
                {
                    context.Entry(exisingEntity).CurrentValues.SetValues(newEntity);
                }
            }
        }
    }
}
