using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.model
{
    public class MasterData : IDataModel
    {
        public int id { get; set; } = 0;
        public string nome { get; set; } = "";
        public string note { get; set; } = "";
        public int last_user { get; set; } = 0;
        public int owner { get; set; } = 0;
        public DateTime time_stamp { get; set; } = DateTime.MaxValue;
        public DateTime insert { get; set; } = DateTime.MaxValue;

        public MasterData(string _source) : base(() => _source) { }

        protected override void SetFields()
        {
            fields.Add(Campi.id, "ID");
            fields.Add(Campi.nome, "Nome");
            fields.Add(Campi.note, "Note");
            fields.Add(Campi.last_user, "IDLastUser");
            fields.Add(Campi.owner, "IDOwner");
            fields.Add(Campi.time_stamp, "TimeStamp");
        }
        public override void BuildModel(DataRow _data)
        {
            id = ModelElaboration.ReadDbData(_data, GetField(Campi.id), 0);
            nome = ModelElaboration.ReadDbData(_data, GetField(Campi.nome), "");
            note = ModelElaboration.ReadDbData(_data, GetField(Campi.note), "");
            last_user = ModelElaboration.ReadDbData(_data, GetField(Campi.last_user), 0);
            owner = ModelElaboration.ReadDbData(_data, GetField(Campi.owner), 0);
            time_stamp = ModelElaboration.ReadDbData(_data, GetField(Campi.time_stamp), DateTime.MaxValue);
            insert = ModelElaboration.ReadDbData(_data, "InsertTime", DateTime.MaxValue);
        }
        public virtual void SetUser(int _id)
        {
            last_user = _id;
            if (owner == 0) owner = _id;
        }
        protected override void SetIdentity()
        {
            identity.Add(Campi.insert);
        }
        protected override void SetPrimaryKeys()
        {
            throw new NotImplementedException();
        }

        public class Campi
        {
            public const string id = "id";
            public const string nome = "nome";
            public const string note = "note";
            public const string last_user = "last_user";
            public const string owner = "owner";
            public const string time_stamp = "time_stamp";
            public const string insert = "insert";
        }
    }
}
