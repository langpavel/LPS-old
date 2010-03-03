using System;
using System.Collections.Generic;

namespace LPS.ToolScript.Parser
{
	public class DBTableTriggers : ICloneable
	{
		private List<IDBTableTrigger> AllTriggers;

		private SortedList<decimal, IDBTableTrigger> BeforeSelect;
		private SortedList<decimal, IDBTableTrigger> AfterSelect;
		private SortedList<decimal, IDBTableTrigger> BeforeInsert;
		private SortedList<decimal, IDBTableTrigger> AfterInsert;
		private SortedList<decimal, IDBTableTrigger> BeforeUpdate;
		private SortedList<decimal, IDBTableTrigger> AfterUpdate;
		private SortedList<decimal, IDBTableTrigger> BeforeDelete;
		private SortedList<decimal, IDBTableTrigger> AfterDelete;

		public DBTableTriggers()
		{
			AllTriggers = new List<IDBTableTrigger>();
			BeforeSelect = null;
			AfterSelect = null;
			BeforeInsert = null;
			AfterInsert = null;
			BeforeUpdate = null;
			AfterUpdate = null;
			BeforeDelete = null;
			AfterDelete = null;
		}

		private void AddPosition(ref SortedList<decimal, IDBTableTrigger> position, IDBTableTrigger trigger)
		{
			if(position == null)
				position = new SortedList<decimal, IDBTableTrigger>();
			position.Add(trigger.Position, trigger);
		}

		private bool HasPosition(DBTriggerPosition pos, IDBTableTrigger trigger)
		{
			return ((pos & trigger.Actions) == pos);
		}

		public void Add(IDBTableTrigger trigger)
		{
			AllTriggers.Add(trigger);

			if(HasPosition(DBTriggerPosition.BeforeSelect, trigger))
				AddPosition(ref BeforeSelect, trigger);
			if(HasPosition(DBTriggerPosition.AfterSelect, trigger))
				AddPosition(ref AfterSelect, trigger);
			if(HasPosition(DBTriggerPosition.BeforeInsert, trigger))
				AddPosition(ref BeforeInsert, trigger);
			if(HasPosition(DBTriggerPosition.AfterInsert, trigger))
				AddPosition(ref AfterInsert, trigger);
			if(HasPosition(DBTriggerPosition.BeforeUpdate, trigger))
				AddPosition(ref BeforeUpdate, trigger);
			if(HasPosition(DBTriggerPosition.AfterUpdate, trigger))
				AddPosition(ref AfterUpdate, trigger);
			if(HasPosition(DBTriggerPosition.BeforeDelete, trigger))
				AddPosition(ref BeforeDelete, trigger);
			if(HasPosition(DBTriggerPosition.AfterDelete, trigger))
				AddPosition(ref AfterDelete, trigger);
		}

		public IDBTableTrigger[] GetAllTriggers()
		{
			return AllTriggers.ToArray();
		}

		public DBTableTriggers Clone()
		{
			DBTableTriggers clone = (DBTableTriggers)this.MemberwiseClone();
			return clone;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
