package services

import (
	"context"
	"log"
	"time"

	"github.com/xarop-pa-toss/ABBA-LM/backend/models"
	"go.mongodb.org/mongo-driver/mongo"
)

type SkillService struct {
	DBClient *mongo.Client
	ctxBg    *context.Context
}

func NewSkillService(client *mongo.Client) *SkillService {
	return &SkillService{DBClient: client}
}

// Create new Skill
func (s *SkillService) CreateSkill(skill models.Skill) {
	collection := s.DBClient.Database("abba_lm").Collection("skills")
	ctx, cancel := context.WithTimeout(*s.ctxBg, 5*time.Second)
	defer cancel() // Ensures Cancel is called to free resources

	_, err := collection.InsertOne(ctx, skill)
	if err != nil {
		log.Fatal(err)
	} else {
		log.Println("INSERT: Skill " + skill.Name + "inserted.")
	}
}
