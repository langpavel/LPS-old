using System;

namespace LPS.ToolScript.Parser
{
	[Flags]
	public enum DBTriggerPosition : int
	{
		None 		 = 0x00,

		BeforeSelect = 0x01,
		AfterSelect  = 0x02,
		BeforeInsert = 0x04,
		AfterInsert  = 0x08,
		BeforeUpdate = 0x10,
		AfterUpdate  = 0x20,
		BeforeDelete = 0x40,
		AfterDelete  = 0x80,

		BeforeModify = 0x54,
		AfterModify  = 0xA8,
		AlwaysBefore = 0x55,
		AlwaysAfter  = 0xAA,
	}
}
