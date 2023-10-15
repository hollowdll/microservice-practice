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

/// Runs the program.
/// Checks the passed commands and arguments.
pub async fn run(cli: &Cli, http_client: &HttpClient) {
    if let Some(Commands::Customer(args)) = &cli.command {
        if let Some(CustomerCommands::Find(args)) = &args.command {
            if args.all {
                match http_client.get_all_customers().await {
                    Ok(customers) => {
                        println!("Number of customers found: {}", customers.len());

                        for customer in customers {
                            println!("{} {}, ID: {}",
                                customer.first_name,
                                customer.last_name,
                                customer.id);
                        }
                    },
                    Err(e) => eprintln!("Failed to get customers: {}", e),
                }
            } else if let Some(id) = args.id {
                match http_client.get_customer_by_id(id).await {
                    Ok(customer) => {
                        println!("{} {}, ID: {}",
                            customer.first_name,
                            customer.last_name,
                            customer.id);
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
            match http_client.add_customer(&customer).await {
                Ok(customer) => {
                    println!("Customer added");
                    println!("ID: {}", customer.id);
                    println!("First name: {}", customer.first_name);
                    println!("Last name: {}", customer.last_name);
                    println!("Email: {}", customer.email);
                    println!("Created at: {}", customer.created_at);
                },
                Err(e) => eprintln!("Failed to add customer: {}", e),
            }
        } else {
            return
        }
    } else if let Some(Commands::Ticket(args)) = &cli.command {
        if let Some(TicketCommands::Find(args)) = &args.command {
            if args.all {
                match http_client.get_all_tickets().await {
                    Ok(data) => {
                        println!("Number of tickets found: {}", data.len());

                        for ticket in data {
                            println!("\nID: {}", ticket.id);
                            println!("Code: {}", ticket.code);
                            println!("Message: {}", ticket.message);
                            println!("Created: {}", ticket.created_at);
                        }
                    },
                    Err(e) => eprintln!("Failed to get tickets: {}", e),
                }
            }
        } else if let Some(TicketCommands::Create(args)) = &args.command {
            let ticket = TicketCreateData { customer_id: args.customer_id };
            
            match http_client.create_ticket(&ticket).await {
                Ok(data) => {
                    println!("{}", data.receipt.message);
                    println!("Receipt created: {}", data.receipt.created_at);
                    println!("Receipt ID: {}", data.receipt.id);
                    println!("Customer: {} {}, ID: {}",
                        data.customer.first_name,
                        data.customer.last_name,
                        data.customer.id);
                    println!("Email address: {}", data.customer.email);
                    println!("Tickets created for this customer: {}", data.receipt.customer_ticket_count);
                    println!("\nTicket ID: {}", data.ticket.id);
                    println!("Code: {}", data.ticket.code);
                    println!("Message: {}", data.ticket.message);
                    println!("Created: {}", data.ticket.created_at);
                },
                Err(e) => eprintln!("Failed to create ticket: {}", e),
            }
        }
    } else {
        return
    }
}