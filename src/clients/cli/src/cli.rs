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
    /// Get customers
    Get(GetCustomerArgs),

    /// Add a customer
    Add(AddCustomerArgs),
}

#[derive(Subcommand)]
pub enum TicketCommands {
    /// Get tickets
    Get(GetTicketArgs),

    /// Create a ticket
    Create(CreateTicketArgs),
}

#[derive(Args)]
pub struct GetCustomerArgs {
    /// Get all customers
    #[arg(short, long)]
    pub all: bool,

    /// Get customer by id
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
pub struct GetTicketArgs {
    /// Get all tickets
    #[arg(short, long)]
    pub all: bool,

    /// Get ticket by id
    #[arg(short, long)]
    pub id: Option<i32>,

    /// Get a specific customer's tickets
    #[arg(short, long)]
    pub customer_id: Option<i32>,
}

#[derive(Args)]
pub struct CreateTicketArgs {
    /// Customer who this ticket should be created for
    #[arg(short, long)]
    pub customer_id: i32,
}
