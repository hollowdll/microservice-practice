use cli::{
    cli::{
        Cli,
        Commands,
        CustomerCommands,
    },
    http::HttpClient,
};
use clap::Parser;
use std::error::Error;

#[tokio::main]
async fn main() -> Result<(), Box<dyn Error>> {
    let cli = Cli::parse();
    let http_client = HttpClient::build();

    match &cli.command {
        Some(Commands::Customer(args)) => {
            match &args.command {
                Some(CustomerCommands::Find(args)) => {
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
                },
                None => return Ok(()),
            }
        },
        None => return Ok(()),
    }

    Ok(())
}
