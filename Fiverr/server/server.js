import express from "express";
import mongoose from "mongoose"

import dotenv from "dotenv"
import userRoute from "./routes/user.js"
import reviewRoute from "./routes/review.js"
import gigRoute from "./routes/gig.js"
import orderRoute from "./routes/order.js"
import messageRoute from "./routes/message.js"
import conversationRoute from "./routes/conversation.js"
import authRoute from "./routes/auth.js"
import cookieParser from "cookie-parser"
import cors from "cors"

 const app=express();
 const port=8000;

 const connectDB = async () => {
	try {
		await mongoose.connect(
			`mongodb+srv://vudo010201:vu161200@cluster0.yjw7cv0.mongodb.net/?retryWrites=true&w=majority`,
			{
				//useCreateIndex: true,
				useNewUrlParser: true,
				useUnifiedTopology: true,
				//useFindAndModify: false
			}
		)

		console.log('MongoDB connected')
	} catch (error) {
		console.log(error.message)
		process.exit(1)
	}
}
connectDB()
const corsOptions ={
    origin:'http://localhost:5173', 
    credentials:true,            //access-control-allow-credentials:true
    optionSuccessStatus:200
}
app.use(cors(corsOptions));
app.use(express.json());
app.use(cookieParser())




app.use("/api/auth",authRoute)
app.use("/api/users",userRoute)
app.use("/api/gigs",gigRoute)
app.use("/api/orders",orderRoute)
app.use("/api/conversations",conversationRoute)
app.use("/api/messages",messageRoute)
app.use("/api/reviews",reviewRoute)

app.use((err,req,res,next)=>{
	const errorStatus=err.status||500;
	const errorMessage=err.message|| "Something went wrong!"

	return res.status(errorStatus).send(errorMessage)
})
 app.listen(8000,()=>{
    console.log("backend is running on " + port)
 })
