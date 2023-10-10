use cli::{
    cli::Cli,
    http::HttpClient,
};
use clap::Parser;
use std::error::Error;

#[tokio::main]
async fn main() -> Result<(), Box<dyn Error>> {
    let cli = Cli::parse();
    let http_client = HttpClient::build();

    cli::run(&cli, &http_client).await;

    Ok(())
}
