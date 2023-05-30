import mongoose from 'mongoose';
const { Schema } = mongoose;

const ConversationSchema = new Schema({
  id:{
    type:String,
    required:true,
    unique:true
  },
  sellerId:{
    type:String,
    required:true
  },
  buyerId:{
    type:String,
    required:true,
  },
  readBySeller:{
    type:String,
    required:true,
  },
  readByBuyer:{
    type:Boolean,
    required:true,
  },
  lastmessage:{
    type:String,
    required:false,
  },
},
{
    timestamps:true
}
);
export default mongoose.model("Conversation",ConversationSchema)