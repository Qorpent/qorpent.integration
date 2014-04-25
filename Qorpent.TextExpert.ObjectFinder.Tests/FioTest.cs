using System;
using System.Linq;
using Comdiv.TextExpert.ObjectFinder.MsSql;
using NUnit.Framework;

namespace Comdiv.TextExpert.ObjectFinder.Tests
{
	[TestFixture]
	public class FioTest
	{
		

		[Test]
		public void Hisamov_Ilya_Bug() {
			var result = new FioTransformer().GetVariants("Хисамов Илья", null);
			Assert.NotNull(result.FirstOrDefault(x=>x.Fio=="ХИСАМОВА ИЛЬИ"));
			result = FioTransformerSqlFunctions.GetFioVariants("Хисамов Илья", 0).OfType<FioVariant>();
			var test = result.FirstOrDefault(x => x.Fio == "ХИСАМОВА ИЛЬИ");
			Assert.NotNull(test);

		}


        [Test]
        public void Bad_Symbols_Hisamov_Ilya_Bug()
        {
            var result = new FioTransformer().GetVariants("Хисамов1   Илья", null);
            Assert.NotNull(result.FirstOrDefault(x => x.Fio == "ХИСАМОВА ИЛЬИ"));
            result = FioTransformerSqlFunctions.GetFioVariants("Хис3амов    Илья", 0).OfType<FioVariant>();
            var test = result.FirstOrDefault(x => x.Fio == "ХИСАМОВА ИЛЬИ");
            Assert.NotNull(test);

        }

	    [Test]
	    public void Abaturov_Alexandr_Kuzmich_in_RODIT()
	    {
            var result = new FioTransformer().GetVariants("Абатуров Александр Кузьмич").First(_=>_.Abbrevated==false && _.LastNameFirst==false && _.Padezh==Padezh.Rodit && _.AddNameUsed);
            Assert.AreEqual("АЛЕКСАНДРА КУЗЬМИЧА АБАТУРОВА",result.Fio);
	    }

		[Test]
		public void Borin_Pavel()
		{
			var result = new FioTransformer().GetVariants("Борин Павел", null);
			Assert.NotNull(result.FirstOrDefault(x => x.Fio == "БОРИНА ПАВЛА"));
			result = FioTransformerSqlFunctions.GetFioVariants("Борин Павел", 0).OfType<FioVariant>();
			var test = result.FirstOrDefault(x => x.Fio == "БОРИНА ПАВЛА");
			Assert.NotNull(test);

		}

		

		[Test]
		public void Borin_Pavel_All()
		{
			var result = new FioTransformer().GetVariants("Борина Мария Анатольевна",new FioTransformerOptions{NotDotedAbbrevations = true,SelfLastNameWeight = 60});
			foreach (var fioVariant in result) {
				Console.WriteLine(fioVariant.Padezh+ " "+ fioVariant.Fio);
			}

		}

		[Test]
		public void Borina_Marina_Bug()
		{
			var result = new FioTransformer().GetVariants("Борина Мария Анатольевна");
			Assert.False(result.Any(x => x.Padezh == Padezh.Rodit && x.Fio == "БОРИНЫ МАРИИ"));
			Assert.True(result.Any(x => x.Padezh == Padezh.Rodit && x.Fio == "БОРИНОЙ МАРИИ"));
		}

		[Test]
		public void Baranov_Arsenij_Tvorit_LastName_Bug()
		{
			var result = new FioTransformer().GetVariants("Баранов Арсений");
			Console.WriteLine(string.Join("\r\n", result.Where(x=>x.Padezh==Padezh.Tvorit).Select(x=>x.Fio)));
			Assert.False(result.Any(x => x.Padezh == Padezh.Tvorit && x.Fio == "БАРАНОВОМ АРСЕНИЕМ"));
			Assert.True(result.Any(x => x.Padezh == Padezh.Tvorit && x.Fio == "БАРАНОВЫМ АРСЕНИЕМ"));
		}


		[Test]
		public void Ustuzhanina_Katerina()
		{
			var result = new FioTransformer().GetVariants("Устюжанина Катерина", null);
			Assert.NotNull(result.FirstOrDefault(x => x.Fio == "УСТЮЖАНИНОЙ ЕКАТЕРИНЫ"));


		}
	}
}
