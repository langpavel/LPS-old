using System;
using NUnit.Framework;

namespace LPS.ToolScript
{
	[TestFixture(Description="Testy ToolScriptu")]
	public class ToolScriptTests
	{
		ToolScriptParser parser = new ToolScriptParser();

		public object Run(string prog)
		{
			StatementList result = parser.Parse(prog);
			if(result == null)
				throw new Exception(String.Format("Parse err: {0}", prog));
			return result.Run(parser);
		}

		[Test]
		public void TestIfReturn()
		{
			Assert.AreEqual("return", Run("return 'return';"));
			Assert.AreEqual("if return", Run("if(true) return 'if return';"));
			Assert.AreEqual(2M, Run(@"
				if(true)
					if(false)
						return 1.0;
					else
						return 2.0;
				else if(true)
					return 3.0;
				else return 4.0;"));
			Assert.AreEqual("if else return", Run("if(false) ; else return 'if else return';"));
			Assert.AreEqual(new object[] { null, 1L, 2M, "3", true, false, null},
				Run("return { null, 1, 2.0, '3', true, false, null};"));
		}

		[Test]
		public void TestForeach1()
		{
			Assert.AreEqual(20L ,Run(@"
				var result = 5;
				foreach(i in {1,2,3,4,5})
				{
					result+=i;
				}
				return result;"));

		}

		[Test]
		public void TestForeach2()
		{
			Assert.AreEqual(15L ,Run(@"
				var result;
				foreach(i in {1,2,3,4,5})
				{
					result+=i;
				}
				return result;"));
		}

		[Test]
		public void TestBlockNesting()
		{
			Assert.AreEqual(3M ,Run(@"
				var result;
				{
					result+=1;
					{
						result+=1.0;
						{ { { { { { result+=1; } } } } } }
					}
				}
				return result;
			"));


			Assert.AreEqual(2M ,Run(@"
				var result;
				{
					result+=1;
					{
						result+=1.0;
						var result;
						{ { { { { {
							result+=1;
							if(result!=1.0) return Format('ERR eq? result = {0}', result);
						} } } } } }
					}
				}
				return result;
			"));

		}

		public void Eq(object val, string code)
		{
			Assert.AreEqual(val, Run(code), code);
		}

		[Test]
		public void TestComparison()
		{
			Eq(true, "return 1==1;");
			Eq(true, "return 1.0==1;");
			Eq(true, "return 1==1.0;");
			Eq(false, "return 1!=1;");
			Eq(false, "return 1.0!=1;");
			Eq(false, "return 1!=1.0;");
			Eq(true, "return !(1!=1.0);");
			Eq(true, "return 'abc'=='abc';");
			Eq(false, "return 'abc'!='abc';");
			Eq(true, "return null==null;");
			Eq(false, "return null=='';");
			Eq(false, "return ''==null;");
		}

		[Test]
		public void TestUnary()
		{
			Eq(false, "return not true;");
			Eq(false, "return ! true;");
			Eq(true, "return not false;");
			Eq(true, "return ! false;");
			Eq(-1, "return -1;");
			Eq(-1, "return -(-(-1));");
			Eq(-1M, "return -1.0;");
			Eq(-1M, "return -(-(-1.0));");
		}

		[Test]
		public void TestTeranryOperator()
		{
			Eq(false, "return false ? true  : false;");
			Eq(true,  "return true  ? true  : false;");
			Eq(true,  "return false ? false : true;");
			Eq(false, "return true  ? false : true;");
		}

		[Test]
		public void TestWhile()
		{
			Eq(100, @"
				var i = 0;
				while(true)
				{
					if(++i >= 100)
						break;
				}
				return i;
			");
			Eq(100, @"
				var i = 0;
				while(++i < 100) ;
				return i;
			");
			Eq(1, @"
				var j = 0;
				do j++; while(false);
				return j;
			");
			Eq(100, @"
				var i = 0;
				var j = 0;
				do j++; while(++i < 100)
				return j;
			");
			Eq(new object[] { 200, 300, 200 }, @"
				var i = 0;
				var j = 0;
				var t = 0;
				do {
					if(j++ < 100)
						continue;
					t++;
				} while(++i < 200)
				return {i, j, t};
			");
		}

		[Test]
		public void TestUnaryIncrementors()
		{
			Eq(new object[] {  0,  1 }, "var i = 0; return {i++, i};");
			Eq(new object[] {  1,  1 }, "var i = 0; return {++i, i};");
			Eq(new object[] {  0, -1 }, "var i = 0; return {i--, i};");
			Eq(new object[] { -1, -1 }, "var i = 0; return {--i, i};");
		}

		[Test]
		public void TestEval()
		{
			Eq("ocekavane", "return eval(\"'ocekavane'\");");
			Eq(2, "return eval('1+1');");
		}

		[Test]
		public void TestFunction()
		{
			Eq("z funkce ! :-)", @"
				function ff(val)
				{
					return val;
				};
				return ff('z funkce ! ') + ff(':-)');
			");
		}

	}
}
