using Lampredotto.Database.query;
using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.model
{
    public abstract class IDataModel : IPrototype
    {
        private Func<string> getsource { get; set; }
        protected string source { get; set; }
        protected Dictionary<string, string> fields { get; set; } = new Dictionary<string, string>();
        protected Dictionary<string, Type> foreign_keys { get; set; }
        protected List<string> primary_keys { get; set; }
        protected List<string> identity { get; set; }
        public Settings settings { get; } = new Settings();
        public IDataModel(Func<string> getsource)
        {
            this.getsource = getsource;
            Initialize();
        }
        public void Initialize()
        {
            fields = new Dictionary<string, string>();
            foreign_keys = new Dictionary<string, Type>();
            primary_keys = new List<string>();
            identity = new List<string>();
            SetFields();
            source = getsource();
            try
            {
                SetForeignKeys();
                SetPrimaryKeys();
                SetIdentity();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("E0065 - Eccezione in Modello.Initialize() : " + ex.Message);
            }
        }
        public string GetField(string name) => fields[name];
        public Dictionary<string, string> GetFields() => fields;
        public T GetForeignKey<T>(IDatabase _reader, string _key, string _fk_id_param = MasterData.Campi.id) where T : IDataModel
        {
            if (foreign_keys.ContainsKey(_key) && foreign_keys[_key].IsSubclassOf(typeof(IDataModel)))
            {
                try
                {
                    IDataModel _model = (IDataModel)Activator.CreateInstance(foreign_keys[_key]);
                    var _list = _reader.RunQuery<List<IDataModel>>(package_CRUD.Enum.select, _model, GetField(_fk_id_param) + "=" + UCode.GetValueByParameterName(this, _key)).Result;
                    if (_list.Count > 0)
                        return (T)_list[0];
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }
        public string GetSource()
        {
            if (settings.IsTest)
                return SourceTestFormat();
            return source;
        }
        public List<string> GetPrimaryKeys()
        {
            if (primary_keys != null && primary_keys.Count > 0) { return primary_keys; }
            else
            {
                if (UCode.GetParameterByName(GetType(), MasterData.Campi.id) != null)
                {
                    return (new string[]{ MasterData.Campi.id }).ToList();
                }
            }

            return null;
        }
        public List<string> GetIdentity()
        {
            return identity;
        }
        public virtual string SourceTestFormat()
        {
            return "_test_" + source;
        }
        public IPrototype Clone()
        {
            IDataModel _clone = (IDataModel)MemberwiseClone();
            _clone.settings.SetReadonly(settings.IsReadonly);
            _clone.settings.SetTest(settings.IsTest);
            return _clone;
        }
        protected abstract void SetFields();
        protected abstract void SetPrimaryKeys();
        protected abstract void SetIdentity();
        public abstract void BuildModel(DataRow _data);
        protected virtual void SetForeignKeys()
        {
        }
        public virtual bool CheckData(string _error = "", string _success = "")
        {
            return true;
        }
        public class Settings
        {
            private bool is_test { get; set; } = false;
            private bool is_readonly { get; set; } = false;
            public void SetTest(bool _test = true) => is_test = _test;
            public void SetReadonly(bool _readonly = true) => is_readonly = _readonly;
            public bool IsTest => is_test;
            public bool IsReadonly => is_readonly;
        }
    }
}
