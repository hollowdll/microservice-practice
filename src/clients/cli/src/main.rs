use cli::{
    cli::{
        Cli,
        Commands,
        CustomerCommands,
    },
    http::HttpClient
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
                        println!("Find all customers");
                    }
                },
                None => return Ok(()),
            }
        },
        None => return Ok(()),
    }

    Ok(())
}
