using Lampredotto.Database.model;
using Lampredotto.Services;
using Lampredotto.Services.frontend;
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
            Add(QueryEnum.select, new Select());
            Add(QueryEnum.insert, new Insert());
            Add(QueryEnum.update, new Update());
            Add(QueryEnum.delete, new Delete());
        }

        public class QueryEnum
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
                if (GetModel() != null)
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
                string _check_data_error = FrontEndHandler.Instance.GetDefault().GetMessage();
                if (GetModel() != null && reference.CheckData(_check_data_error) && !GetModel().settings.IsReadonly)
                {
                    try
                    {
                        var campi = ModelElaboration.GetFieldList(reference);
                        var valori = ModelElaboration.GetValueList(reference);

                        if (campi.Count == valori.Count)
                        {
                            SetQueryString("INSERT INTO " + GetModel().GetSource() + " (" + CodingUtilities.GetStringList(campi) + ") VALUES (" + CodingUtilities.GetStringList(valori) + ")");
                        }
                        else
                        {
                            throw ExceptionSingleton.Instance.GetException("Il numero dei campi e dei valori per il modello " + GetModel().GetSource() + " non corrispondono.", ExceptionSingleton.Encoder.fwsections.database, "QueryInsert", 16);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ExceptionSingleton.Instance.GetException(ex.Message, ExceptionSingleton.Encoder.fwsections.database, "QueryInsert", 13);
                    }
                }
                else
                {
                    throw ExceptionSingleton.Instance.GetException(_check_data_error, ExceptionSingleton.Encoder.fwsections.database, "QueryInsert", 10);
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
                {
                    string _check_data_error = FrontEndHandler.Instance.GetDefault().GetMessage();
                    if (GetModel() != null && GetModel().CheckData(_check_data_error) && !GetModel().settings.IsReadonly)
                    {
                        try
                        {
                            var _campi = ModelElaboration.GetFieldList(reference);
                            var _valori = ModelElaboration.GetValueList(reference);

                            if (_campi.Count == _valori.Count)
                            {
                                var _string_update = "";
                                for (var i = 0; i <= _campi.Count - 1; i++) { _string_update += _campi[i] + "=" + _valori[i] + ","; }
                                _string_update = _string_update.TrimEnd(',');
                                SetQueryString("UPDATE " + GetModel().GetSource() + " SET " + _string_update);
                            }
                            else throw ExceptionSingleton.Instance.GetException("Il numero dei campi e dei valori per il modello " + GetModel().GetSource() + " non corrispondono.", ExceptionSingleton.Encoder.fwsections.database, "QueryUpdate", 19);

                        }
                        catch (Exception ex)
                        {
                            throw ExceptionSingleton.Instance.GetException(ex.Message, ExceptionSingleton.Encoder.fwsections.database, "QueryUpdate", 12);
                        }
                    }
                    else
                        throw ExceptionSingleton.Instance.GetException(_check_data_error, ExceptionSingleton.Encoder.fwsections.database, "QueryUpdate", 10);
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
                if (GetModel() != null && !GetModel().settings.IsReadonly)
                {
                    try
                    {
                        query.SetQueryString("DELETE FROM " + GetModel().GetSource());
                    }
                    catch (Exception ex)
                    {
                        throw ExceptionSingleton.Instance.GetException(ex.Message, ExceptionSingleton.Encoder.fwsections.database, "QueryDelete", 10);
                    }
                }
                else
                {
                    throw ExceptionSingleton.Instance.GetException("Impossibile accedere al modello di riferimento.", ExceptionSingleton.Encoder.fwsections.database, "QueryDelete", 8);
                }
            }
        }
    }
}
