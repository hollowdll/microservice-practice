use cli::{
    Cli,
    Commands,
    CustomerCommands,
};
use http::HttpClient;
use customer::CustomerCreateData;

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
    } else {
        return
    }
}