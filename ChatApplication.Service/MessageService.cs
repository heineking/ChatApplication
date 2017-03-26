using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Service
{
    public class MessageService : IMessageWriter
    {
        private readonly IUnitOfWork _uow;
        private readonly IModelMapper _modelMapper;

        public MessageService(IModelMapper mapper, IUnitOfWork uow)
        {
            _modelMapper = mapper;
            _uow = uow;
        }
        public void Save(Message message)
        {
            // todo: handle exception that bubbles up from the DAL...
            var messageRecord = _modelMapper.MessageToMessageRecord(message);
            _uow.Messages.Add(messageRecord);
            _uow.SaveChanges();
        }
    }
}
