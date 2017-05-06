import React from 'react';
import Divider from 'material-ui/Divider';
import './MessageList.css';

const Message = ({ message }) => {
  const { text, userName, postedDate } = message;
  return (
    <div className="message-card">
      <div className="top">
        <span>{userName}</span>
        <span>{new Date(postedDate).toLocaleDateString()}</span>
      </div>
      <div className="main">
        {text}
      </div>
    </div>
  );
}

const MessageList = props => {
  const { messages = [] } = props;
  return (
    <div className="message-list">
      {messages.map(message =>
        <div key={message.messageId}>
          <Message message={message} />
          <Divider />
        </div>
      )}
    </div>
  );
};

export default MessageList;
