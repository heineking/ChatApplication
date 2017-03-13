using ChatApplication.Data.Contracts.Models;
using ChatApplication.Security.Contracts;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Infrastructure.Contracts
{
    public interface IModelMapper
    {
        MessageRecord MessageToMessageRecord(Message message);
        Message MessageRecordToMessage(MessageRecord messageRecord);

        RoomRecord RoomToRoomRecord(Room room);
        Room RoomRecordToRoom(RoomRecord roomRecord);

        UserRecord UserToUserRecord(User user);
        User UserRecordToUser(UserRecord userRecord);

        LoginRecord LoginToLoginRecord(Login login);
        Login LoginRecordToLogin(LoginRecord loginRecord);

    }
}
