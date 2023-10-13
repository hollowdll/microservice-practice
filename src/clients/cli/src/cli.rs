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
    /// Ticket related features
    Ticket(TicketArgs),
}

#[derive(Args)]
pub struct CustomerArgs {
    #[command(subcommand)]
    pub command: Option<CustomerCommands>,
}

#[derive(Args)]
pub struct TicketArgs {
    #[command(subcommand)]
    pub command: Option<TicketCommands>,
}

#[derive(Subcommand)]
pub enum CustomerCommands {
    /// Find customers
    Find(FindCustomerArgs),

    /// Add a customer
    Add(AddCustomerArgs),
}

#[derive(Subcommand)]
pub enum TicketCommands {
    /// Find tickets
    Find(FindTicketArgs),

    /// Create a ticket
    Create(CreateTicketArgs),
}

#[derive(Args)]
pub struct FindCustomerArgs {
    /// Find all customers
    #[arg(short, long)]
    pub all: bool,

    /// Customer id
    #[arg(short, long)]
    pub id: Option<i32>,
}

#[derive(Args)]
pub struct AddCustomerArgs {
    /// Customer first name
    #[arg(short, long)]
    pub first_name: String,

    /// Customer last name
    #[arg(short, long)]
    pub last_name: String,

    /// Customer email address
    #[arg(short, long)]
    pub email: String,
}

#[derive(Args)]
pub struct FindTicketArgs {
    /// Find all tickets
    #[arg(short, long)]
    pub all: bool,
}

#[derive(Args)]
pub struct CreateTicketArgs {
    /// Customer id
    #[arg(short, long)]
    pub customer_id: i32,
}
