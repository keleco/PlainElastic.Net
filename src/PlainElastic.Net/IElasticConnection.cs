﻿
namespace PlainElastic.Net
{
    public interface IElasticConnection
    {

        string DefaultHost { get; set; }

        int DefaultPort { get; set; }

        OperationResult Get(string command);

        OperationResult Post(string command, string jsonData = null);

        OperationResult Put(string command, string jsonData = null);

        OperationResult Delete(string command, string jsonData = null);

    }
}
