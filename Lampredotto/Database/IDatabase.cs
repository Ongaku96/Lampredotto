using Lampredotto.Database.connection;
using Lampredotto.Database.model;
using Lampredotto.Database.query;
using Lampredotto.Database.query.decorator;
using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database
{
    public abstract class IDatabase
    {
        private IConnection connection { get; set; }
        private Dictionary<string, QueryBuilder> map_query { get; set; } = new Dictionary<string, QueryBuilder>();
        private Dictionary<string, IDataModel> map_models { get; set; } = new Dictionary<string, IDataModel>();

        public IDatabase(IConnection _connect)
        {
            Initialize(_connect);
        }
        private void Initialize(IConnection _connect)
        {
            SetConnection(_connect);
            AddPackage(new package_CRUD());
            SetupQuery();
            SetupModels();
        }

        public void SetConnection(IConnection _connect) => connection = _connect;
        public IConnection GetConnection() => connection;

        public void AddQuery(string _key, QueryBuilder _builder) => map_query.Add(_key, _builder);
        public void AddModel(string _key, IDataModel _model) => map_models.Add(_key, _model);
        public QueryBuilder GetQuery(string _key) => map_query[_key];
        public IDataModel GetModel(string _key) => map_models[_key];

        public void AddPackage(IPackage<QueryBuilder> _package)
        {
            foreach(var _item in _package.GetData())
            {
                map_query.Add(_item.Key, _item.Value);
            }
        }
        public void AddPackage(IPackage<IDataModel> _package)
        {
            foreach (var _item in _package.GetData())
            {
                map_models.Add(_item.Key, _item.Value);
            }
        }

        public async Task<T> RunQuery<T>(QueryBuilder _query_builder, IDataModel _model = null, int _timeout = 30)
        {
            var _run_my_query = Task.Run(() =>
            {
                try
                {
                    if (_query_builder != null)
                    {
                        QueryDirector _query = new QueryDirector();
                        _query.SetBuilder(_query_builder);
                        _query.Initialize(_model, connection.GetConnectionString(), _timeout);
                        return _query.RunQuery();
                    }

                    return default(T);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        );
            var _result = await _run_my_query;
            return (T)_result;
        }
        public async Task<T> RunQuery<T>(string  _query_key, IDataModel _model = null, string _filter = "", string _sort = "", int _timeout = 30)
        {
            var _run_my_query = Task.Run(() =>
            {
                try
                {
                    var _query_builder = GetQuery(_query_key);
                    if (_query_builder != null)
                    {
                        _query_builder = new QueryFilterDecorator(_query_builder, _filter);
                        _query_builder = new QuerySortDecorator(_query_builder, _sort);
                        return RunQuery<T>(_query_builder, _model, _timeout).Result;
                    }
                    return default(T);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        );
            var _result = await _run_my_query;
            return (T)_result;
        }

        protected abstract void SetupModels();
        protected abstract void SetupQuery();
    }
}
