package server

import (
	"log"
	"net/http"
)
type Server struct {
	listenAddr string
}

func NewServer(listenAddr string) *Server {
	return &Server{
		listenAddr: listenAddr,
	}
}

func (s *Server) Start() error {

	http.HandleFunc("/skills", s.)

	httpServer, err := router.ListenAndServe(s.listenAddr, nil)
	if err != nil {
		log.Fatal(err)
	}
	
	return httpServer
}