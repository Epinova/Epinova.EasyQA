using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.DataInterfaces
{
    public interface IQaInstanceRepository
    {
        QaInstance Get(int id);
        IEnumerable<QaInstance> GetAll();
        QaInstance SaveQaInstance(QaInstance qa);
    }
}