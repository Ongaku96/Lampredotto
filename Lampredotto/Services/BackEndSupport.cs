using Lampredotto.Database;
using Lampredotto.Database.model;
using Lampredotto.Database.query;
using Lampredotto.Services.frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Services
{
    public sealed class BackEndSupport
    {
        public static FrontEndData<string> UpdateData(string _database_key, IDataModel _model, Func<string> _success = null, Func<string> _fail = null)
        {
            string _output;
            if (_model != null)
            {
                try
                {
                    var _database = DatabaseFactory.Instance.Get(_database_key);
                    if (_database != null)
                    {
                        var _op = _database.RunQuery<bool>(package_CRUD.QueryEnum.update, _model, ModelElaboration.GetFilterByPrimaryKeys(_model)).Result;
                        if (_op)
                        {
                            _output = _success == null ? FrontEndHandler.Instance.GetMessage("Request completed with success.") : _success.Invoke();
                            return (FrontEndData<string>)FrontEndHandler.Instance.GetDefault().Clone(_output, FrontEndData<string>.ResultEnum._success, _op.ToString());
                        }
                        else
                        {
                            _output = "No data were updated.";
                        }
                    }
                    else
                    {
                        _output = "Error! Impossible to load the database references.";
                    }
                }
                catch (Exception e)
                {
                    _output = e.Message;
                }
            }
            else
            {
                _output = _fail == null ? FrontEndHandler.Instance.GetMessage("Error! Unable to complete the task.") : _fail.Invoke();
            }
            return (FrontEndData<string>)FrontEndHandler.Instance.GetDefault().Clone(_output, FrontEndData<string>.ResultEnum._error, false.ToString());
        }
        public static FrontEndData<string> DeleteData(string _database_key, IDataModel _model, Func<string> _success = null, Func<string> _fail = null)
        {
            string _output;
            if (_model != null)
            {
                try
                {
                    var _database = DatabaseFactory.Instance.Get(_database_key);
                    if (_database != null)
                    {
                        var _op = _database.RunQuery<bool>(package_CRUD.QueryEnum.delete, _model, ModelElaboration.GetFilterByPrimaryKeys(_model)).Result;
                        if (_op)
                        {
                            _output = _success == null ? FrontEndHandler.Instance.GetMessage("Request completed with success.") : _success.Invoke();
                            return (FrontEndData<string>)FrontEndHandler.Instance.GetDefault().Clone(_output, FrontEndData<string>.ResultEnum._success, _op.ToString());
                        }
                        else
                        {
                            _output = "No data were deleted.";
                        }
                    }
                    else
                    {
                        _output = "Error! Impossible to load the database references.";
                    }
                }
                catch (Exception e)
                {
                    _output = e.Message;
                }
            }
            else
            {
                _output = _fail == null ? FrontEndHandler.Instance.GetMessage("Error! Unable to complete the task.") : _fail.Invoke();
            }
            return (FrontEndData<string>)FrontEndHandler.Instance.GetDefault().Clone(_output, FrontEndData<string>.ResultEnum._error, false.ToString());
        }
        public static FrontEndData<int> InsertData(string _database_key, IDataModel _model, Func<string> _success = null, Func<string> _fail = null)
        {
            string _output;
            if (_model != null)
            {
                try
                {
                    var _database = DatabaseFactory.Instance.Get(_database_key);
                    if (_database != null)
                    {
                        var _op = _database.RunQuery<int>(package_CRUD.QueryEnum.insert, _model, ModelElaboration.GetFilterByPrimaryKeys(_model)).Result;
                        if (_op > 0)
                        {
                            _output = _success == null ? FrontEndHandler.Instance.GetMessage("Request completed with success.") : _success.Invoke();
                            return (FrontEndData<int>)FrontEndHandler.Instance.GetDefault().Clone(_output, FrontEndData<string>.ResultEnum._success, _op);
                        }
                        else
                        {
                            _output = "No data were insert.";
                        }
                    }
                    else
                    {
                        _output = "Error! Impossible to load the database references.";
                    }
                }
                catch (Exception e)
                {
                    _output = e.Message;
                }
            }
            else
            {
                _output = _fail == null ? FrontEndHandler.Instance.GetMessage("Error! Unable to complete the task.") : _fail.Invoke();
            }
            return (FrontEndData<int>)FrontEndHandler.Instance.GetDefault().Clone(_output, FrontEndData<string>.ResultEnum._error, 0);
        }

    }
}
