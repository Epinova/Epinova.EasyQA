using System;
using System.Collections.Generic;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Core.DataInterfaces
{
    public interface IQaInstanceRepository
    {
        IList<QaInstance> GetAll();
        QaInstance GetById(int id);
        int SaveQaInstance(QaInstance qa);
    }
}