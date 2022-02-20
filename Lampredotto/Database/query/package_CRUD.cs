using Lampredotto.Database.model;
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
                string _check_data_error = ResponseHandler.Instance().GetMessage(ResponseHandler.ResponseEnum.error_form);
                if (GetModel() != null && reference.CheckData(_check_data_error) && !GetModel().settings.IsReadonly)
                {
                    try
                    {
                        var campi = ModelElaboration.GetFieldList(reference);
                        var valori = ModelElaboration.GetValueList(reference);

                        if (campi.Count == valori.Count)
                        {
                            var qs = "INSERT INTO " + GetModel().GetSource() + " (" + UCode.GetStringList(campi) + ") VALUES (" + UCode.GetStringList(valori) + ")";
                            query.SetQueryString(qs);
                        }
                        else
                        {
                            throw ExceptionHandler.Instance.GetException("Il numero dei campi e dei valori per il modello " + GetModel().GetSource() + " non corrispondono.", ExceptionHandler.Encoder.fwsections.database, "QueryInsert", 16);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ExceptionHandler.Instance.GetException(ex.Message, ExceptionHandler.Encoder.fwsections.database, "QueryInsert", 13);
                    }
                }
                else
                {
                    throw ExceptionHandler.Instance.GetException(_check_data_error, ExceptionHandler.Encoder.fwsections.database, "QueryInsert", 10);
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
                    string _check_data_error = ResponseHandler.Instance().GetMessage(ResponseHandler.ResponseEnum.error_form);
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

                                var qs = "UPDATE " + GetModel().GetSource() + " SET " + _string_update;
                                query.SetQueryString(qs);
                            }
                            else throw ExceptionHandler.Instance.GetException("Il numero dei campi e dei valori per il modello " + GetModel().GetSource() + " non corrispondono.", ExceptionHandler.Encoder.fwsections.database, "QueryUpdate", 19);

                        }
                        catch (Exception ex)
                        {
                            throw ExceptionHandler.Instance.GetException(ex.Message, ExceptionHandler.Encoder.fwsections.database, "QueryUpdate", 12);
                        }
                    }
                    else
                        throw ExceptionHandler.Instance.GetException(_check_data_error, ExceptionHandler.Encoder.fwsections.database, "QueryUpdate", 10);
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
                        throw ExceptionHandler.Instance.GetException(ex.Message, ExceptionHandler.Encoder.fwsections.database, "QueryDelete", 10);
                    }
                }
                else
                {
                    throw ExceptionHandler.Instance.GetException("Impossibile accedere al modello di riferimento.", ExceptionHandler.Encoder.fwsections.database, "QueryDelete", 8);
                }
            }
        }
    }
}
