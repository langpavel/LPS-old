using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace LPS.ToolScript.Tests
{
	public class TestDisposable : IDisposable
	{
		public bool disposed;
		public TestDisposable()
		{
			disposed = false;
		}

		public void Dispose()
		{
			disposed = true;
		}
	}

	[TestFixture(Description="Testy ToolScriptu")]
	public class ToolScriptTests
	{
		ToolScriptParser parser;

		public ToolScriptTests()
		{
			parser = new ToolScriptParser();
			Log.Add(new TextLogger(Console.Out, Verbosity.Debug));
		}

		public object Run(string prog)
		{
			StatementList result = parser.Parse(prog);
			if(result == null)
				throw new Exception(String.Format("Parse err: {0}", prog));
			Context context = Context.CreateRootContext(parser);
			context.InitVariable("this", this);
			return result.RunAsMain(context);
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
				Run("return [ null, 1, 2.0, '3', true, false, null];"));
		}

		[Test]
		public void TestForeach1()
		{
			Assert.AreEqual(20L ,Run(@"
				var result = 5;
				foreach(i in [1,2,3,4,5])
				{
					result+=i;
				}
				return result;"));

		}

		[Test]
		public void TestForeach2()
		{
			Assert.AreEqual(15L ,Run(@"
				var result = 0;
				foreach(i in [1,2,3,4,5])
				{
					result+=i;
				}
				return result;"));
		}

		[Test]
		public void TestBlockNesting()
		{
			Assert.AreEqual(3M ,Run(@"
				var result = 0;
				{
					result+=1;
					{
						result+=1.0;
						{ { { { { { result+=1; } } } } } }
					}
				}
				return result;
			"));
		}

		[Test]
		public void TestBlockNesting2()
		{
			Assert.AreEqual(2M ,Run(@"
				var result = 0;
				{
					result+=1;
					{
						result+=1.0;
						var result = 0;
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
				return [i, j, t];
			");
		}

		[Test]
		public void TestUnaryIncrementors()
		{
			Eq(new object[] {  0,  1 }, "var i = 0; return [i++, i];");
			Eq(new object[] {  1,  1 }, "var i = 0; return [++i, i];");
			Eq(new object[] {  0, -1 }, "var i = 0; return [i--, i];");
			Eq(new object[] { -1, -1 }, "var i = 0; return [--i, i];");
		}

		[Test]
		public void TestUnaryIncrementors2()
		{
			Eq(new object[] {  0,  1,  2 }, "var i = 0; return [i++, i++, i++];");
			Eq(new object[] {  1,  2,  3 }, "var i = 0; return [++i, ++i, ++i];");
			Eq(new object[] {  0, -1, -2 }, "var i = 0; return [i--, i--, i--];");
			Eq(new object[] { -1, -2, -3 }, "var i = 0; return [--i, --i, --i];");
		}

		[Test]
		public void TestHashTable()
		{
			Eq(new object[] {"aaa", "bbc", "abc"}, "var d = {'A': 'aaa', 'B': 'bbb'}; d['C'] = 'abc'; d['B'] = 'bbc'; return [d['A'], d['B'], d['C']];");
		}

		[Test]
		public void TestArrayTable()
		{
			Eq(new object[] {"ccc", "bbb"}, "var array = ['aaa', 'bbb']; array[0] = 'ccc'; return array;");
		}

		[Test]
		public void TestArrayTable2()
		{
			Eq(new object[] {"ccc", "bbb"}, "var array = [['aaa', 3.0], ['bbb',['aaa', 'bbb']]]; array[1][1][0] = 'ccc'; return array[1][1];");
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
			Eq("z funkce !", @"
				function ff()
				{
					return 'z funkce !';
				};
				return ff();
			");
		}

		[Test]
		public void TestFunction2()
		{
			Eq("z funkce ! :-)", @"
				function ff(val)
				{
					return val;
				};
				return ff('z funkce ! ') + ff(':-)');
			");
		}

		[Test]
		public void PropertyGet()
		{
			Eq(3, "return 'abc'.Length;");
		}

		[Test]
		public void NativeInvokeTest1()
		{
			Eq("3", "return 'abc'.Length.ToString();");
			Eq(1, "return 'abc'.Length.ToString().Length;");
			Eq("1", "return 'abc'.Length.ToString().Length.ToString();");
		}

		[Test]
		public void NativeInvokeTest2()
		{
			Eq("abc", "return this.ToString('abc');");
			Eq(this.ToString(), "return this.ToString();");
		}

		[Test]
		public void NativeInvokeTest3()
		{
			Eq(3, "return this.Add(1, cast 2 as System.Int32);");
			Eq(this.ToString(), "return this.ToString();");
			//Eq(3, "return this.Add(1, 2);"); // FAIL - type downcast needed
		}

		//[Test]
		public void NativeInvokeTest4()
		{
			// FAIL - array cast needed
			Eq("abcde", "return this.Format('a{0}cd{1}',['b','e']);");
		}

		//[Test]
		public void NativeInvokeWithParamsTest()
		{
			// FAIL - array cast needed
			Eq("abcde", "return this.Format('a{0}cd{1}', 'b','e');");
		}

		public string TestField;
		public string TestProp { get; set; }

		[Test]
		public void NativeInvokeTest5()
		{
			aa.aa = new BB();
			Eq("aaa", "return this.TestProp = 'aaa';");
			Eq("aaa", "return this.TestProp;");
			Eq("aaa", "return this.TestField = 'aaa';");
			Eq("aaa", "return this.TestField;");
			Eq("aaa", "return this.aa.TestProp = 'aaa';");
			Eq("aaa", "return this.aa.TestProp;");
			Eq("aaa", "return this.aa.TestField = 'aaa';");
			Eq("aaa", "return this.aa.TestField;");
			Eq("aaa", "return this.aa.aa.TestProp = 'aaa';");
			Eq("aaa", "return this.aa.aa.TestProp;");
			Eq("aaa", "return this.aa.aa.TestField = 'aaa';");
			this.aa.aa.TestField = "cc";
			Eq("cc", "return this.aa.aa.TestField;");
		}

		[Test]
		public void TestNewKeyword()
		{
			Eq(typeof(long), "return (new System.Int64()).GetType();");
		}

		[Test]
		public void TestUsingDisposable()
		{
			Eq(typeof(TestDisposable), "return (new LPS.ToolScript.Tests.TestDisposable()).GetType();");
			Eq(false, "using(var obj = new LPS.ToolScript.Tests.TestDisposable()) return obj.disposed;");
			Eq(true, "using(var obj = new LPS.ToolScript.Tests.TestDisposable()) ; return obj.disposed;");
		}

		public AA aa = new BB();

		public class AA
		{
			public AA() {}
			public AA aa;
			public string TestField;
			public string TestProp { get; set; }
		}
		class BB : AA { public BB() {} }

		[Test]
		public void ConversionTests()
		{
			AA a = new AA();
			BB b = new BB();
			Assert.AreEqual(true, b.GetType().IsSubclassOf(a.GetType()));
			Assert.AreEqual(false, b.GetType().IsSubclassOf(b.GetType()));
			//Convert.ChangeType(b, typeof(AA));
		}

		public string Format(string s, params object[] args)
		{
			return String.Format(s, args);
		}

		public string ToString(string asdf)
		{
			return asdf;
		}

		public long Add(long a, int b)
		{
			return a+b;
		}

		public static ToolScriptTests operator+ (ToolScriptTests a, ToolScriptTests b)
		{
			return a;
		}

		//[Test]
		public void FindMembersTest()
		{
	        Object objTest = this;
	        Type objType = objTest.GetType ();
	        MemberInfo[] arrayMemberInfo;
	        try
	        {
	            //Find all static or public methods in the Object class that match the specified name.
	            arrayMemberInfo = objType.FindMembers(MemberTypes.Method | MemberTypes.All,
	                BindingFlags.Public | BindingFlags.Static /*| BindingFlags.Instance | BindingFlags.NonPublic*/,
	                FindAll, null);
	
	            for(int index=0;index < arrayMemberInfo.Length ;index++)
	                Console.WriteLine ("Member: " + arrayMemberInfo[index].ToString());
	        }
	        catch (Exception e)
	        {
	            Console.WriteLine ("Exception : " + e.ToString() );
	        }
			throw new Exception("show");
		}

		private string PTEXT;
		public string TEXT { get { return PTEXT;}  set { PTEXT = value;} }

	    public static bool FindAll(MemberInfo objMemberInfo, Object objSearch)
	    {
			return true;
	    }

		public static DBNull NULL { get { return DBNull.Value; } }

		[Test]
		public void RangeOperatorTest()
		{
			Eq(true,  "return null in <null, null>;");
			Eq(true,  "return null in <null, null);");
			Eq(true,  "return null in (null, null>;");
			Eq(true,  "return null in (null, null);");

			Eq(true,  "return this.NULL in <this.NULL, this.NULL>;");
			Eq(true,  "return this.NULL in <this.NULL, this.NULL);");
			Eq(true,  "return this.NULL in (this.NULL, this.NULL>;");
			Eq(true,  "return this.NULL in (this.NULL, this.NULL);");

			Eq(true,  "return 5 in <null, 5>;");
			Eq(false, "return 5 in <null, 5);");
			Eq(true,  "return 5 in (null, 5>;");
			Eq(false, "return 5 in (null, 5);");

			Eq(true,  "return 5 in <5, null>;");
			Eq(true,  "return 5 in <5, null);");
			Eq(false, "return 5 in (5, null>;");
			Eq(false, "return 5 in (5, null);");

			Eq(true,  "return 5 in <5, 5>;");
			Eq(false, "return 5 in <5, 5);");
			Eq(false, "return 5 in (5, 5>;");
			Eq(false, "return 5 in (5, 5);");

			Eq(true,  "return 5 in <5, 10>;");
			Eq(true,  "return 5 in <5, 10);");
			Eq(false, "return 5 in (5, 10>;");
			Eq(false, "return 5 in (5, 10);");

			Eq(true,  "return 10 in <5, 10>;");
			Eq(false, "return 10 in <5, 10);");
			Eq(true,  "return 10 in (5, 10>;");
			Eq(false, "return 10 in (5, 10);");

			Eq(true,  "return 5.0 in <5.0, 5.0>;");
			Eq(false, "return 5.0 in <5.0, 5.0);");
			Eq(false, "return 5.0 in (5.0, 5.0>;");
			Eq(false, "return 5.0 in (5.0, 5.0);");

			Eq(true,  "return 5.0 in <5.0, 10.0>;");
			Eq(true,  "return 5.0 in <5.0, 10.0);");
			Eq(false, "return 5.0 in (5.0, 10.0>;");
			Eq(false, "return 5.0 in (5.0, 10.0);");

			Eq(true,  "return 10.0 in <5.0, 10.0>;");
			Eq(false, "return 10.0 in <5.0, 10.0);");
			Eq(true,  "return 10.0 in (5.0, 10.0>;");
			Eq(false, "return 10.0 in (5.0, 10.0);");
		}

		[Test]
		public void TestDateTimeLiteral()
		{
			Eq(new DateTime(2000, 2, 1), "return d2000-02-01;");
			Eq(new DateTime(2000, 2, 1,12,1,0), "return d2000-02-01t12:01;");
			Eq(new DateTime(2000, 2, 1,12,1,1), "return d2000-02-01t12:01:01;");
			Eq(new DateTime(2000, 2, 1,12,1,1,123), "return d2000-02-01t12:01:01.123;");

			Eq(new DateTime(1, 1, 1, 12, 1, 0), "return t12:01;");
			Eq(new DateTime(1, 1, 1, 12, 1, 2), "return t12:01:02;");
			Eq(new DateTime(1, 1, 1, 12, 1, 2, 3), "return t12:01:02.003;");

			Eq(DateTime.Today, "return today;");
			Eq(DateTime.Today.AddDays(1), "return tomorrow;");
			Eq(DateTime.Today.AddDays(-1), "return yesterday;");
		}

		[Test]
		public void TestTimespanLiteral()
		{
			Eq(new TimeSpan(0, 0, 0, 0, 1), "return 0.001s;");
			Eq(new TimeSpan(0, 0, 0, 1, 2), "return 1.002s;");
			Eq(new TimeSpan(0, 0, 0, 1, 0), "return 1s;");
			Eq(new TimeSpan(0, 0, 1, 0, 0), "return 1m;");
			Eq(new TimeSpan(0, 0, 1, 2, 0), "return 1m2s;");
			Eq(new TimeSpan(0, 1, 2, 3, 0), "return 1h2m3s;");
			Eq(new TimeSpan(1, 2, 3, 4, 0), "return 1d2h3m4s;");
			Eq(new TimeSpan(1, 2, 3, 4, 5), "return 1d2h3m4.005s;");
		}

		[Test]
		public void TestTimeOperation()
		{
			Eq(new TimeSpan(0, 0, 0, 0, 1), "return 0.001s;");
			Eq(new TimeSpan(0, 0, 0, 1, 2), "return 1.002s;");
			Eq(new TimeSpan(0, 0, 0, 1, 0), "return 1s;");
			Eq(new TimeSpan(0, 0, 1, 0, 0), "return 1m;");
			Eq(new TimeSpan(0, 0, 1, 2, 0), "return 1m2s;");
			Eq(new TimeSpan(0, 1, 2, 3, 0), "return 1h2m3s;");
			Eq(new TimeSpan(1, 2, 3, 4, 0), "return 1d2h3m4s;");
			Eq(new TimeSpan(1, 2, 3, 4, 5), "return 1d2h3m4.005s;");

			Eq(new TimeSpan(0, 0, 1, 1, 0), "return 1s + 1m;");

			Eq(DateTime.Today.AddMinutes(1), "return today + 1m;");
		}
	}
}
