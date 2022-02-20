using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query
{
    public class package_CRUD : IPackage<QueryBuilder>
    {
        protected override void SetupPackage()
        {
            Add(Enum.select, new Select());
            Add(Enum.insert, new Insert());
            Add(Enum.update, new Update());
            Add(Enum.delete, new Delete());
        }

        public class Enum
        {
            public const string select = "select";
            public const string insert = "insert";
            public const string update = "update";
            public const string delete = "delete";
        }

        protected class Select : QueryBuilder
        {
            public override void BuildOperationData()
            {
                SetOperationData(QueryElaborationFactory.Instance.Get(QueryElaborationFactory.QueryElaborationEnum.datamodel, GetModel()));
            }
            public override void BuildQueryString()
            {
                if(GetModel() != null)
                {
                    SetQueryString("SELECT * FROM " + GetModel().GetSource());
                }
            }
        }
        protected class Insert : QueryBuilder
        {
            public override void BuildOperationData()
            {
                SetOperationData(QueryElaborationFactory.Instance.Get(QueryElaborationFactory.QueryElaborationEnum.scalar, GetModel()));
            }

            public override void BuildQueryString()
            {
                if (GetModel() != null)
                {
                    SetQueryString("INSERT INTO " + GetModel().GetSource());
                }
            }
        }
        protected class Update : QueryBuilder
        {
            public override void BuildOperationData()
            {
                SetOperationData(QueryElaborationFactory.Instance.Get(QueryElaborationFactory.QueryElaborationEnum.command, GetModel()));
            }

            public override void BuildQueryString()
            {
                if (GetModel() != null)
                {
                    SetQueryString("UPDATE " + GetModel().GetSource());
                }
            }
        }
        protected class Delete : QueryBuilder
        {
            public override void BuildOperationData()
            {
                SetOperationData(QueryElaborationFactory.Instance.Get(QueryElaborationFactory.QueryElaborationEnum.command, GetModel()));
            }

            public override void BuildQueryString()
            {
                if (GetModel() != null)
                {
                    SetQueryString("DELETE FROM" + GetModel().GetSource());
                }
            }
        }
    }
}
