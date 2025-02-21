package controllers

import (
	"context"
	"encoding/json"
	"log"
	"net/http"
	"time"

	"go.mongodb.org/mongo-driver/bson"

	"github.com/xarop-pa-toss/ABBA-LM/backend/cmd/models"
	"go.mongodb.org/mongo-driver/mongo"
)

type SkillController struct {
	DBClient *mongo.Client
	ctxBg    *context.Context
}

func NewSkillController(client *mongo.Client) *SkillController {
	return &SkillController{DBClient: client}
}

// Create new Skill
func (s *SkillController) CreateSkill(response http.ResponseWriter, request *http.Request) {
	response.Header().Add("content-type", "application/json")

	var skill models.Skill
	json.NewDecoder(request.Body).Decode(&skill)

	collection := s.DBClient.Database("abba_lm").Collection("skills")
	ctx, cancel := context.WithTimeout(*s.ctxBg, 5*time.Second)
	defer cancel()

	result, err := collection.InsertOne(ctx, skill)
	if err != nil {
		log.Fatal(err)
	} else {
		log.Println("INSERT: Skill " + skill.Name + " inserted.")
	}

	json.NewEncoder(response).Encode(result)
}

func (s *SkillController) GetSingleSkill(response http.ResponseWriter, request *http.Request) {
	response.Header().Add("content-type", "application/json")

	var skill models.Skill
	json.NewDecoder(request.Body).Decode(&skill)

	collection := s.DBClient.Database("abba_lm").Collection("skills")
	ctx, cancel := context.WithTimeout(*s.ctxBg, 10*time.Second)
	defer cancel()

	result := collection.FindOne(ctx, skill)
	if result.Err() == mongo.ErrNoDocuments {
		response.WriteHeader(http.StatusInternalServerError)
		response.Write([]byte(`{ "message": "Skill ` + skill.Name + ` was not found."}`))
		return
	} else {
		log.Println("Found Skill " + skill.Name)
	}

	json.NewEncoder(response).Encode(result)
}

func (s *SkillController) GetAllSkills(response http.ResponseWriter, request *http.Request) {
	response.Header().Add("content-type", "application/json")

	var skills []models.Skill
	json.NewDecoder(request.Body).Decode(&skills)

	collection := s.DBClient.Database("abba_lm").Collection("skills")
	ctx, cancel := context.WithTimeout(*s.ctxBg, 10*time.Second)
	defer cancel()

	cursor, err := collection.Find(ctx, bson.M{})
	if err != nil {
		response.WriteHeader(http.StatusInternalServerError)
		response.Write([]byte(`{ "message": "No skills were found. ` + err.Error() + `"}`))
		return
	} else {
		log.Println("Found Skills")
	}
	defer cursor.Close(ctx)

	for cursor.Next(ctx) {
		var skill models.Skill
		cursor.Decode(&skill)
		skills = append(skills, skill)
	}
	if err := cursor.Err(); err != nil {
		response.WriteHeader(http.StatusInternalServerError)
		response.Write([]byte(`{ "message": "` + err.Error() + `"}`))
		return
	}

	json.NewEncoder(response).Encode(skills)
}

// func (s *SkillController) UpdateSkill(response http.ResponseWriter, request *http.Request) {
// 	response.Header().Add("content-type", "application/json")

// 	var skill models.Skill
// 	json.NewDecoder(request.Body).Decode(&skill)

// 	collection := s.DBClient.Database("abba_lm").Collection("skills")
// 	ctx, cancel := context.WithTimeout(*s.ctxBg, 5*time.Second)
// 	defer cancel()

// 	result := collection.FindOneAndUpdate(ctx, skill)
// 	if err != nil {
// 		log.Fatal(err)
// 	} else {
// 		log.Println("INSERT: Skill " + skill.Name + " inserted.")
// 	}

// 	json.NewEncoder(response).Encode(result)
// }
