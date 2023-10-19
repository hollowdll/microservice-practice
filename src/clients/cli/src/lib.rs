use std::time::Instant;
use cli::{
    Cli,
    Commands,
    CustomerCommands,
    TicketCommands,
};
use http::HttpClient;
use customer::CustomerCreateData;
use ticket::TicketCreateData;

pub mod cli;
pub mod customer;
pub mod ticket;
pub mod receipt;
pub mod http;
pub mod config;

fn print_response_time(instant: &Instant) {
    println!("Response time: {} ms", instant.elapsed().as_millis());
}

/// Runs the program.
/// Checks the passed commands and arguments.
pub async fn run(cli: &Cli, http_client: &HttpClient) {
    if let Some(Commands::Customer(args)) = &cli.command {
        if let Some(CustomerCommands::Get(args)) = &args.command {
            if args.all {
                let now = Instant::now();

                match http_client.get_all_customers().await {
                    Ok(data) => {
                        print_response_time(&now);
                        println!("Number of customers found: {}", data.len());

                        for customer in data {
                            println!("\n{}", customer);
                        }
                    },
                    Err(e) => eprintln!("Failed to get customers: {}", e),
                }
            } else if let Some(id) = args.id {
                let now = Instant::now();

                match http_client.get_customer_by_id(id).await {
                    Ok(data) => {
                        print_response_time(&now);
                        println!("{}", data);
                    },
                    Err(e) => eprintln!("Failed to get customer: {}", e),
                }
            }
        } else if let Some(CustomerCommands::Add(args)) = &args.command {
            let customer = CustomerCreateData::new(
                &args.first_name,
                &args.last_name,
                &args.email
            );
            let now = Instant::now();

            match http_client.add_customer(&customer).await {
                Ok(data) => {
                    print_response_time(&now);
                    println!("Customer added");
                    println!("{}", data);
                },
                Err(e) => eprintln!("Failed to add customer: {}", e),
            }
        } else {
            return
        }
    } else if let Some(Commands::Ticket(args)) = &cli.command {
        if let Some(TicketCommands::Get(args)) = &args.command {
            if args.all {
                let now = Instant::now();

                match http_client.get_all_tickets().await {
                    Ok(data) => {
                        print_response_time(&now);
                        println!("Number of tickets found: {}", data.len());

                        for ticket in data {
                            println!("\n{}", ticket);
                        }
                    },
                    Err(e) => eprintln!("Failed to get tickets: {}", e),
                }
            } else if let Some(id) = args.id {
                let now = Instant::now();

                match http_client.get_ticket_by_id(id).await {
                    Ok(data) => {
                        print_response_time(&now);
                        println!("{}", data);
                    },
                    Err(e) => eprintln!("Failed to get ticket: {}", e),
                }
            } else if let Some(customer_id) = args.customer_id {
                let now = Instant::now();

                match http_client.get_customer_tickets(customer_id).await {
                    Ok(data) => {
                        print_response_time(&now);
                        println!("Number of tickets found: {}", data.len());

                        for ticket in data {
                            println!("\n{}", ticket);
                        }
                    },
                    Err(e) => eprintln!("Failed to get customer's tickets: {}", e),
                }
            }
        } else if let Some(TicketCommands::Create(args)) = &args.command {
            let ticket = TicketCreateData { customer_id: args.customer_id };
            let now = Instant::now();

            match http_client.create_ticket(&ticket).await {
                Ok(data) => {
                    print_response_time(&now);
                    println!("Ticket created");
                    println!("Receipt ID: {}", data.receipt.id);
                    println!("Message: {}", data.receipt.message);
                    println!("Created: {}", data.receipt.created_at);
                    println!("Customer: {} {}, ID: {}",
                        data.customer.first_name,
                        data.customer.last_name,
                        data.customer.id);
                    println!("Email address: {}", data.customer.email);
                    println!("Tickets created for the customer: {}", data.receipt.customer_ticket_count);
                    println!("\n{}", data.ticket);
                },
                Err(e) => eprintln!("Failed to create ticket: {}", e),
            }
        }
    } else {
        return
    }
}