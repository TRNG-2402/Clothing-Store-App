type PaginationControlsProps = {
    pageNumber: number;
    totalPages: number;
    onPageChange: (page: number) => void;
};

function PaginationControls({
    pageNumber,
    totalPages,
    onPageChange
}: PaginationControlsProps) {
    return (
        <div>
            <button
                onClick={() => onPageChange(pageNumber - 1)}
                disabled={pageNumber <= 1}
            >
                Previous
            </button>

            <span>
                Page {pageNumber} of {totalPages}
            </span>

            <button
                onClick={() => onPageChange(pageNumber + 1)}
                disabled={pageNumber >= totalPages}
            >
                Next
            </button>
        </div>
    );
}

export default PaginationControls;