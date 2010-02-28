using System;
using LPS.ToolScript.Parser;
using NUnit.Framework;

namespace LPS.ToolScript.Tests
{
	[TestFixture]
	public class ToolScriptGuiTest : ToolScriptTestBase
	{
		[Test]
		public void TestWindow()
		{
			Assert.IsInstanceOfType(
				typeof(WindowExpression),
				Run(@"return
					window
						vbox
							['AAA']
							hbox spacing=5; padding=5; ['BBB'] ['CCC'] packend; ['DDD'] end
							['EEE']
						end;
				"));
		}
	}
}
