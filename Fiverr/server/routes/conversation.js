import express from "express";
 
import{
    getConversations,createConversation,getSingleConversation,updateConversation
}from "../controllers/conversationController.js";
import {verifyToken} from "../middleware/jwt.js"
const router=express.Router();

router.get("/",verifyToken,getConversations);
router.post("/",verifyToken,createConversation);
router.get("/single/:id",verifyToken,getSingleConversation)
router.put("/:id",verifyToken,updateConversation)

export default router;
