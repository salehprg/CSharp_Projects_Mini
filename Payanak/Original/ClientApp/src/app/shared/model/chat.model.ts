export class ChatModel {
  public avatar: string;
  public chatClass: string;
  public imagePath: string;
  public time: string;
  public messages: string[];
  public messageType: string;
  public ticketId: number;

  public constructor(avatar: string, chatClass: string, imagePath: string, time: string, messages: string[], messageType: string) {
    this.avatar = avatar;
    this.chatClass = chatClass;
    this.imagePath = imagePath;
    this.time = time;
    this.messages = messages;
    this.messageType = messageType;
    this.ticketId = -1;
  }
}

