# Overview

In this project I practice microservices architecture and technologies related to it.

The system I am building is a ticket system where you can create tickets with random codes for customers and get a receipt for every created ticket.

System components:
- `API gateway`
- `Customer service`
- `Ticket service`
- `Receipt service`
- `CLI client`

Services and API gateway are all dockerized and the whole backend system can be run with Docker Compose. Instructions for that later. Every service runs in its own container. I am learning Kubernetes so I will hopefully use that in the future.

I am building the system primarily with C#, but the CLI client is built with Rust. Rust is an amazing language to build CLI apps. It has really great performance, ecosystem, and useful libraries.

Right now the system uses HTTP APIs internally. I am learning gRPC and hopefully all internal communication will be switched to gRPC services. API gateway remains to use HTTP API.

I am currently learning:
- gRPC remote procedure call framework

In the future:
- Authentication
- Kubernetes deployment