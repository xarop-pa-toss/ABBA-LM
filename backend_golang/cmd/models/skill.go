package models

import "go.mongodb.org/mongo-driver/bson/primitive"

type Skill struct {
	ID          primitive.ObjectID `json: "id", bson:"_id,omitempty"`
	Name        string
	Category    SkillCategory 		`json: "id", bson:"category,omitempty"`
	Description string
}

func ValidateSkill(s *Skill) bool { return true }

type SkillCategory string
const (
	CategoryGeneral  SkillCategory = "General"
	CategoryStrength SkillCategory = "Strength"
	CategoryAgility  SkillCategory = "Agility"
	CategoryPassing  SkillCategory = "Passing"
	CategoryMutation SkillCategory = "Mutation"
	CategoryTrait    SkillCategory = "Traits"
)
