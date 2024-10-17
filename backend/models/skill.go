package models

import "go.mongodb.org/mongo-driver/bson/primitive"

type Skill struct {
	ID          primitive.ObjectID `bson:"_id,omitempty"`
	Name        string
	Category    SkillCategory `bson:"category,omitempty"`
	Description string
}

type SkillCategory string

const (
	CategoryGeneral  SkillCategory = "General"
	CategoryStrength SkillCategory = "Strength"
	CategoryAgility  SkillCategory = "Agility"
	CategoryPassing  SkillCategory = "Passing"
	CategoryMutation SkillCategory = "Mutation"
	CategoryTrait    SkillCategory = "Traits"
)
