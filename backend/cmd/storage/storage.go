package storage

import "github.com/xarop-pa-toss/ABBA-LM/backend/cmd/models"

type Storage interface {
	Get(string) *models.Skill
}

