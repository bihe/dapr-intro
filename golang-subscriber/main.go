package main

import (
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net/http"
	"os"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
)

// --------------------------------------------------------------------------
// types
// --------------------------------------------------------------------------

// Message defines an object which is received
type Message struct {
	Topic string         `json:"topic,omitempty"`
	Data  MessagePayload `json:"data,omitempty"`
}

// MessagePayload specifies the structure of the data within a message
type MessagePayload struct {
	Message string `json:"message,omitempty"`
}

// --------------------------------------------------------------------------
// handler
// --------------------------------------------------------------------------

func procMessage(route string) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		msg, err := getMessage(r.Body)
		defer r.Body.Close()
		if err != nil {
			http.Error(w, err.Error(), 500)
			return
		}
		log.Printf("📜 via '%s', for '%s' with message '%s'", route, msg.Topic, msg.Data.Message)
		w.WriteHeader(http.StatusOK)
	}
}

func getMessage(r io.Reader) (Message, error) {
	var (
		msg Message
		err error
		dec *json.Decoder
	)
	dec = json.NewDecoder(r)
	if err = dec.Decode(&msg); err != nil {
		return Message{}, fmt.Errorf("error decoding payload: %v", err)
	}
	return msg, nil
}

// --------------------------------------------------------------------------

func main() {
	port := os.Getenv("PORT")
	if port == "" {
		port = "3000"
	}
	r := chi.NewRouter()
	r.Use(middleware.Logger)

	r.Post("/receive_all", procMessage("/receive_all"))
	r.Post("/receive_b", procMessage("/receive_b"))

	fmt.Printf("🚀 up and running @ %s\n", fmt.Sprintf(":%s", port))

	http.ListenAndServe(fmt.Sprintf(":%s", port), r)
}
