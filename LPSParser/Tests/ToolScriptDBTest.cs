using System;
using NUnit.Framework;

namespace LPS.ToolScript.Tests
{
	[TestFixture]
	public class ToolScriptDBTest : ToolScriptTestBase
	{
		[Test]
		public void TestCase()
		{
			Run(@"return
				database sklad
				{
					template table all_base
					{
						ts datetime;
					}

					template table base template all_base
					{
						id_user_create foreign user(id);
						dt_create datetime;
						id_user_modify foreign user(id);
						dt_modify datetime;
					}

					table table1
					{
						id primary;
						test varchar;
					}
				};
			");
		}
	}
}
