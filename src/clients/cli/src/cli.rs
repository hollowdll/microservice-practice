use clap::{
    Parser,
    Subcommand,
    Args,
};

/// Program to interact with the microservice-practice project backend system.
#[derive(Parser)]
#[command(author, version, about, long_about = None)]
pub struct Cli {
    #[command(subcommand)]
    pub command: Option<Commands>,
}

#[derive(Subcommand)]
pub enum Commands {
    /// Customer related features
    Customer(CustomerArgs),
}

#[derive(Args)]
pub struct CustomerArgs {
    #[command(subcommand)]
    pub command: Option<CustomerCommands>,
}

#[derive(Subcommand)]
pub enum CustomerCommands {
    /// Find customers
    Find(FindCustomerArgs),
}

#[derive(Args)]
pub struct FindCustomerArgs {
    /// Find all customers
    #[arg(short, long)]
    pub all: bool,
}
