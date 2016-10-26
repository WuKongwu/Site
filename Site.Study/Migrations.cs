using Orchard.Data.Migration;
using System;

namespace Site.Study {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder
                .CreateTable("UserPartRecord",
                             table => table
                                          .Column<int>("Id", column => column.PrimaryKey().Identity())
                                          .Column<string>("Name", c => c.WithLength(2048))
                                          .Column<string>("PassWord", c => c.WithLength(2048))
                                          .Column<int>("Status")
                                          .Column<DateTime>("CreateTime", c => c.WithLength(256)));
                //.CreateTable("ActionRecord",
                //             table => table
                //                          .Column<int>("Id", column => column.PrimaryKey().Identity())
                //                          .Column<string>("Area")
                //                          .Column<string>("Controller")
                //                          .Column<string>("Action"));
            return 1;
        }

    }
}