﻿using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Requests.Events;
using Microsoft.EntityFrameworkCore;

namespace KGP.TicketApp.Repositories
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        #region Constructors

        public EventRepository(DatabaseContext context) : base(context) { }

        public override Event? GetById(Guid id)
        {
            return DatabaseContext.Set<Event>()
                .Include(e => e.Organizer)
                .FirstOrDefault(e => e.Id == id);
        }

        public override void Create(Event entity)
        {
            var organizer = DatabaseContext.Set<Organizer>().Find(entity.Organizer.Id)!;

            if (organizer.Events == null)
            {
                organizer.Events = new() { entity };
            }
            else
            {
                organizer.Events.Add(entity);
            }

            DatabaseContext.Set<Organizer>().Update(organizer);
        }

        public List<Event> GetByIdList(IEnumerable<Guid> ids)
        {
            return DatabaseContext.Set<Event>()
                .Where(e => ids.Contains(e.Id))
                .ToList();
        }

        public List<Event> GetByOrganizerId(Guid id)
        {
            return DatabaseContext.Set<Event>()
                .Where(e => e.Organizer.Id == id)
                .ToList();
        }

        public List<Event> GetByFilterFromRequest(GetEventsRequest request)
        {
            var query = DatabaseContext
                .Set<Event>()
                .Include(e => e.Organizer)
                .AsQueryable();

            if (request.IsFull.HasValue)
            {
                query = request.IsFull.Value
                    ? query.Where(ev => ev.ParticipantsList.Count >= ev.ParticipantsLimit)
                    : query.Where(ev => ev.ParticipantsList.Count < ev.ParticipantsLimit);
            }

            if (request.DateFrom.HasValue)
            {
                query = query.Where(ev => ev.Date >= request.DateFrom);
            }

            if (request.DateTo.HasValue)
            {
                query = query.Where(ev => ev.Date <= request.DateTo);
            }

            // TODO: Location

            return query.ToList();
        }

        #endregion

        #region Interface methods

        #endregion
    }
}
