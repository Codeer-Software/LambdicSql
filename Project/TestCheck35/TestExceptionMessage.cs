using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCheck35
{
    //TODO Expression message.
    class TestExceptionMessage
    {
        /*
         *         [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_Continue_Union_All_Exp()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var exp = Sql<DB>.Create(db => true);
            var target = Sql<DB>.Create(db =>
                Union(exp).Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION ALL
SELECT *
FROM tbl_staff");
        }*/

        //And Select Args too.
    }
}
